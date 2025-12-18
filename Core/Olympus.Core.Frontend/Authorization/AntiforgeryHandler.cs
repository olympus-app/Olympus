namespace Olympus.Core.Frontend.Authorization;

public class AntiforgeryHandler(AntiforgeryService antiforgery) : DelegatingHandler {

	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {

		var token = await antiforgery.GetAsync();

		if (!string.IsNullOrEmpty(token)) request.Headers.TryAddWithoutValidation(Headers.Antiforgery, token);

		return await base.SendAsync(request, cancellationToken);

	}

}
