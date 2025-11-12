using System.Net;

namespace Olympus.Core.Backend.Exceptions;

public abstract class AppBackendException(AppErrors.Keys key = AppErrors.Keys.UnknownError, string? resource = null, string? identifier = null, HttpStatusCode statuscode = HttpStatusCode.InternalServerError, Exception? inner = null) : AppException(key, resource, identifier, inner) {

	public HttpStatusCode? StatusCode { get; init; } = statuscode;

}
