using Microsoft.AspNetCore.Components.Authorization;

namespace Olympus.Core.Frontend.Authorization;

public class AntiforgeryHandler(AntiforgeryService antiforgery, AuthenticationStateProvider authStateProvider) : DelegatingHandler {

	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {

		if (HttpVerbs.UnsafeVerbs.Contains(request.Method.ToString())) {

			var authState = await authStateProvider.GetAuthenticationStateAsync();

			if (authState.User.IsAuthenticated) {

				var token = await antiforgery.GetAsync();

				if (!string.IsNullOrEmpty(token)) request.Headers.TryAddWithoutValidation(HttpHeaders.Antiforgery, token);

			}

		}

		return await base.SendAsync(request, cancellationToken);

	}

}
