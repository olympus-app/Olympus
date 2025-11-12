using System.Net;
using System.Security.Claims;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;

namespace Olympus.Api.Protection;

public class RateLimiterConfigurator : IConfigureOptions<RateLimiterOptions> {

	public void Configure(RateLimiterOptions options) {

		options.RejectionStatusCode = HttpStatusCode.TooManyRequests.Value;

		options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext => {

			var userClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
			var partitionKey = userClaim?.Value ?? "anonymous";

			return RateLimitPartition.GetTokenBucketLimiter(partitionKey,
				factory: partition => new TokenBucketRateLimiterOptions {
					TokenLimit = 250,
					TokensPerPeriod = 25,
					QueueLimit = 5,
					ReplenishmentPeriod = 1.Minutes(),
					AutoReplenishment = true
				});

		});

		options.AddConcurrencyLimiter(policyName: "HeavyEndpoint", options => {
			options.PermitLimit = 3;
			options.QueueLimit = 1;
		});

	}

}
