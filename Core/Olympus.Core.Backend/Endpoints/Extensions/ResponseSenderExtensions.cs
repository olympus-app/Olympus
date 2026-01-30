using Microsoft.AspNetCore.Authentication;

namespace Olympus.Core.Backend.Endpoints;

public static class ResponseSenderExtensions {

	extension(IResponseSender sender) {

		public Task<Void> SuccessAsync(CancellationToken cancellationToken = default) {

			return sender.HttpContext.Response.SendStatusCodeAsync(HttpStatusCode.NoContent.Value, cancellationToken);

		}

		public Task<Void> SuccessAsync<TResponse>(TResponse response, CancellationToken cancellationToken = default) where TResponse : class, IResponse {

			return sender.HttpContext.Response.SendAsync(response, HttpStatusCode.OK.Value, null, cancellationToken);

		}

		public Task<Void> CreatedAsync(CancellationToken cancellationToken = default) {

			return sender.HttpContext.Response.SendStatusCodeAsync(HttpStatusCode.Created.Value, cancellationToken);

		}

		public Task<Void> CreatedAsync<TResponse>(TResponse response, CancellationToken cancellationToken = default) where TResponse : class, IResponse {

			return sender.HttpContext.Response.SendAsync(response, HttpStatusCode.Created.Value, null, cancellationToken);

		}

		public Task<Void> UpdatedAsync(CancellationToken cancellationToken = default) {

			return sender.HttpContext.Response.SendStatusCodeAsync(HttpStatusCode.OK.Value, cancellationToken);

		}

		public Task<Void> UpdatedAsync<TResponse>(TResponse response, CancellationToken cancellationToken = default) where TResponse : class, IResponse {

			return sender.HttpContext.Response.SendAsync(response, HttpStatusCode.OK.Value, null, cancellationToken);

		}

		public Task<Void> DeletedAsync(CancellationToken cancellationToken = default) {

			return sender.HttpContext.Response.SendStatusCodeAsync(HttpStatusCode.NoContent.Value, cancellationToken);

		}

		public Task<Void> NotModifiedAsync(CancellationToken cancellationToken = default) {

			return sender.HttpContext.Response.SendStatusCodeAsync(HttpStatusCode.NotModified.Value, cancellationToken);

		}

		public Task<Void> ProblemAsync(HttpStatusCode statusCode = HttpStatusCode.InternalServerError, CancellationToken cancellationToken = default) {

			var result = new ProblemResult() {
				Status = statusCode.Value,
				Message = statusCode.Humanized,
			};

			return sender.HttpContext.Response.SendAsync(result, result.Status, null, cancellationToken);

		}

		public Task<Void> ProblemAsync(string detail, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, CancellationToken cancellationToken = default) {

			var result = new ProblemResult() {
				Status = statusCode.Value,
				Message = statusCode.Humanized,
				Detail = detail,
			};

			return sender.HttpContext.Response.SendAsync(result, result.Status, null, cancellationToken);

		}

		public Task<Void> BadRequestAsync(CancellationToken cancellationToken = default) {

			return sender.ProblemAsync(HttpStatusCode.BadRequest, cancellationToken);

		}

		public Task<Void> BadRequestAsync(string detail, CancellationToken cancellationToken = default) {

			return sender.ProblemAsync(detail, HttpStatusCode.BadRequest, cancellationToken);

		}

		public Task<Void> ForbiddenAsync(CancellationToken cancellationToken = default) {

			return sender.ProblemAsync(HttpStatusCode.Forbidden, cancellationToken);

		}

		public Task<Void> ForbiddenAsync(string detail, CancellationToken cancellationToken = default) {

			return sender.ProblemAsync(detail, HttpStatusCode.Forbidden, cancellationToken);

		}

		public Task<Void> NotFoundAsync(CancellationToken cancellationToken = default) {

			return sender.ProblemAsync(HttpStatusCode.NotFound, cancellationToken);

		}

		public Task<Void> NotFoundAsync(string detail, CancellationToken cancellationToken = default) {

			return sender.ProblemAsync(detail, HttpStatusCode.NotFound, cancellationToken);

		}

		public Task<Void> ErrorAsync(CancellationToken cancellationToken = default) {

			return sender.ProblemAsync(HttpStatusCode.InternalServerError, cancellationToken);

		}

		public Task<Void> ErrorAsync(string detail, CancellationToken cancellationToken = default) {

			return sender.ProblemAsync(detail, HttpStatusCode.InternalServerError, cancellationToken);

		}

		public Task<Void> ConflictAsync(CancellationToken cancellationToken = default) {

			return sender.ProblemAsync(ErrorsStrings.Values.ConcurrencyConflict, HttpStatusCode.Conflict, cancellationToken);

		}

		public Task<Void> ConflictAsync(string detail, CancellationToken cancellationToken = default) {

			return sender.ProblemAsync(detail, HttpStatusCode.Conflict, cancellationToken);

		}

		public Task<Void> UnauthorizedAsync(CancellationToken cancellationToken = default) {

			return sender.ProblemAsync(ErrorsStrings.Values.ConcurrencyConflict, HttpStatusCode.Unauthorized, cancellationToken);

		}

		public Task<Void> UnauthorizedAsync(string detail, CancellationToken cancellationToken = default) {

			return sender.ProblemAsync(detail, HttpStatusCode.Unauthorized, cancellationToken);

		}

		public Task<Void> ChallengeAsync(AuthenticationProperties properties, IList<string> schemes) {

			return sender.HttpContext.Response.SendResultAsync(Results.Challenge(properties, schemes));

		}

		public Task<Void> LocalRedirectAsync(string location, bool isPermanent = false) {

			return sender.HttpContext.Response.SendRedirectAsync(location, isPermanent, false);

		}

		public Task<Void> ExternalRedirectAsync(string location, bool isPermanent = false) {

			return sender.HttpContext.Response.SendRedirectAsync(location, isPermanent, true);

		}

		public Task<Void> StringAsync(string value, CancellationToken cancellationToken = default) {

			return sender.HttpContext.Response.SendStringAsync(value, HttpStatusCode.OK.Value, ContentTypes.Text, cancellationToken);

		}

		public Task<Void> StringAsync(string value, string contentType = ContentTypes.Text, CancellationToken cancellationToken = default) {

			return sender.HttpContext.Response.SendStringAsync(value, HttpStatusCode.OK.Value, contentType, cancellationToken);

		}

		public Task<Void> ExceptionAsync(Exception exception, CancellationToken cancellationToken = default) {

			return sender.ProblemAsync(exception.Message, HttpStatusCode.InternalServerError, cancellationToken);

		}

		public Task<Void> ExceptionAsync(Exception exception, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, CancellationToken cancellationToken = default) {

			return sender.ProblemAsync(exception.Message, statusCode, cancellationToken);

		}

		public Task<Void> ItemAsync<TResponse>(TResponse item, string cacheControl = ResponseCache.PrivateRevalidate, CancellationToken cancellationToken = default) where TResponse : class, IResponse {

			var etag = EntityTag.From(item);

			if (EntityTag.IfNoneMatch(sender.HttpContext.Request.Headers.ETag, etag)) return sender.NotModifiedAsync(cancellationToken);

			if (!string.IsNullOrEmpty(etag)) sender.HttpContext.Response.Headers.ETag = etag;

			if (!string.IsNullOrEmpty(cacheControl)) sender.HttpContext.Response.Headers.CacheControl = cacheControl;

			return sender.HttpContext.Response.SendAsync(item, HttpStatusCode.OK.Value, null, cancellationToken);

		}

		public Task<Void> ListAsync<TResponse>(IEnumerable<TResponse> items, string cacheControl = ResponseCache.PrivateRevalidate, CancellationToken cancellationToken = default) where TResponse : class, IResponse {

			var result = new ListResult<TResponse>(items);

			var etag = EntityTag.From(result);

			if (EntityTag.IfNoneMatch(sender.HttpContext.Request.Headers.ETag, etag)) return sender.NotModifiedAsync(cancellationToken);

			if (!string.IsNullOrEmpty(etag)) sender.HttpContext.Response.Headers.ETag = etag;

			if (!string.IsNullOrEmpty(cacheControl)) sender.HttpContext.Response.Headers.CacheControl = cacheControl;

			return sender.HttpContext.Response.SendAsync(result, HttpStatusCode.OK.Value, null, cancellationToken);

		}

		public Task<Void> PageAsync<TResponse>(int page, int pageSize, IEnumerable<TResponse> items, int totalCount, string cacheControl = ResponseCache.PrivateRevalidate, CancellationToken cancellationToken = default) where TResponse : class, IResponse {

			var result = new PageResult<TResponse>(page, pageSize, items, totalCount);

			var etag = EntityTag.From(result);

			if (EntityTag.IfNoneMatch(sender.HttpContext.Request.Headers.ETag, etag)) return sender.NotModifiedAsync(cancellationToken);

			if (!string.IsNullOrEmpty(etag)) sender.HttpContext.Response.Headers.ETag = etag;

			if (!string.IsNullOrEmpty(cacheControl)) sender.HttpContext.Response.Headers.CacheControl = cacheControl;

			return sender.HttpContext.Response.SendAsync(result, HttpStatusCode.OK.Value, null, cancellationToken);

		}

		public Task<Void> FileAsync(Stream stream, string fileName, long? fileLength, string contentType = ContentTypes.Stream, string? etag = null, DateTimeOffset? lastModified = null, string cacheControl = ResponseCache.PrivateRevalidate, bool enableRangeProcessing = false, CancellationToken cancellationToken = default) {

			if (EntityTag.IfNoneMatch(sender.HttpContext.Request.Headers.ETag, etag)) return sender.NotModifiedAsync(cancellationToken);

			if (!string.IsNullOrEmpty(etag)) sender.HttpContext.Response.Headers.ETag = etag;

			if (!string.IsNullOrEmpty(cacheControl)) sender.HttpContext.Response.Headers.CacheControl = cacheControl;

			return sender.HttpContext.Response.SendStreamAsync(stream, fileName, fileLength, contentType, lastModified, enableRangeProcessing, cancellationToken);

		}

	}

}
