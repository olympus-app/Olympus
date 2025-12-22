namespace Olympus.Core.Backend.Entities;

public static class ResponseSenderExtensions {

	extension(IResponseSender sender) {

		public Task<Void> ProblemAsync(string detail, CancellationToken cancellationToken = default) {

			var result = new ProblemResult() {
				Status = HttpStatusCode.Conflict.Value,
				Message = HttpStatusCode.Conflict.Humanized,
				Detail = detail,
			};

			return sender.HttpContext.Response.SendAsync(result, result.Status, null, cancellationToken);

		}

		public Task<Void> CreatedAsync(CancellationToken cancellationToken = default) {

			return sender.HttpContext.Response.SendStatusCodeAsync(HttpStatusCode.Created.Value, cancellationToken);

		}

		public Task<Void> CreatedAsync<TResponse>(TResponse response, CancellationToken cancellationToken = default) {

			return sender.HttpContext.Response.SendAsync(response, HttpStatusCode.Created.Value, null, cancellationToken);

		}

		public Task<Void> UpdatedAsync(CancellationToken cancellationToken = default) {

			return sender.HttpContext.Response.SendStatusCodeAsync(HttpStatusCode.OK.Value, cancellationToken);

		}

		public Task<Void> UpdatedAsync<TResponse>(TResponse response, CancellationToken cancellationToken = default) {

			return sender.HttpContext.Response.SendAsync(response, HttpStatusCode.OK.Value, null, cancellationToken);

		}

		public Task<Void> DeletedAsync(CancellationToken cancellationToken = default) {

			return sender.HttpContext.Response.SendStatusCodeAsync(HttpStatusCode.NoContent.Value, cancellationToken);

		}

		public Task<Void> NotModifiedAsync(CancellationToken cancellationToken = default) {

			return sender.HttpContext.Response.SendStatusCodeAsync(HttpStatusCode.NotModified.Value, cancellationToken);

		}

		public Task<Void> ConflictAsync(CancellationToken cancellationToken = default) {

			return sender.ProblemAsync(ErrorsStrings.Values.ConcurrencyConflict, cancellationToken);

		}

		public Task<Void> ConflictAsync(string detail, CancellationToken cancellationToken = default) {

			return sender.ProblemAsync(detail, cancellationToken);

		}

	}

}
