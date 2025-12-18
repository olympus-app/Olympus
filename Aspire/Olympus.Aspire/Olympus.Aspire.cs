using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Olympus.Aspire;

public static class Aspire {

	private const string HealthEndpointPath = "/health";

	private const string AlivenessEndpointPath = "/alive";

	public static TBuilder AddAspire<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder {

		builder.ConfigureOpenTelemetry();

		builder.AddDefaultHealthChecks();

		builder.Services.AddServiceDiscovery();

		builder.Services.ConfigureHttpClientDefaults(static http => {
			http.AddStandardResilienceHandler();
			http.AddServiceDiscovery();
		});

		return builder;

	}

	public static TBuilder ConfigureOpenTelemetry<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder {

		builder.Logging.AddOpenTelemetry(static logging => {
			logging.IncludeFormattedMessage = true;
			logging.IncludeScopes = true;
		});

		builder.Services.AddOpenTelemetry()
			.WithMetrics(static metrics => {
				metrics.AddAspNetCoreInstrumentation()
					.AddHttpClientInstrumentation()
					.AddRuntimeInstrumentation();
			})
			.WithTracing(tracing => {
				tracing.AddSource(builder.Environment.ApplicationName)
					.AddAspNetCoreInstrumentation(static tracing =>
						tracing.Filter = static context =>
							!context.Request.Path.StartsWithSegments(HealthEndpointPath)
							&& !context.Request.Path.StartsWithSegments(AlivenessEndpointPath)
					)
					.AddHttpClientInstrumentation()
					.AddEntityFrameworkCoreInstrumentation()
					.AddRedisInstrumentation();
			});

		builder.AddOpenTelemetryExporters();

		return builder;

	}

	private static TBuilder AddOpenTelemetryExporters<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder {

		var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

		if (useOtlpExporter) builder.Services.AddOpenTelemetry().UseOtlpExporter();

		return builder;

	}

	public static TBuilder AddDefaultHealthChecks<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder {

		builder.Services.AddRequestTimeouts(static timeouts =>
			timeouts.AddPolicy("HealthChecks", TimeSpan.FromSeconds(5))
		);

		builder.Services.AddOutputCache(static caching =>
			caching.AddPolicy("HealthChecks", build: static policy => policy.Expire(TimeSpan.FromSeconds(15)))
		);

		builder.Services.AddHealthChecks().AddCheck("self", static () => HealthCheckResult.Healthy(), ["live"]);

		return builder;

	}

	public static WebApplication MapDefaultEndpoints(this WebApplication app) {

		app.MapHealthChecks(HealthEndpointPath);

		app.MapHealthChecks(AlivenessEndpointPath, new HealthCheckOptions() { Predicate = static reg => reg.Tags.Contains("live") });

		return app;

	}

}
