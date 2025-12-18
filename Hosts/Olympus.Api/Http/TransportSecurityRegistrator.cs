using Microsoft.Extensions.Hosting;

namespace Olympus.Api.Http;

public static class TransportSecurityRegistrator {

	public static void UseTransportSecurityMiddleware(this WebApplication app) {

		if (app.Environment.IsProduction()) app.UseHsts();

		app.UseHttpsRedirection();

	}

}
