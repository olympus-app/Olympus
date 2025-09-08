using System.Net;

namespace Olympus.Kernel;

public abstract class AppException(AppErrors.Keys key = AppErrors.Keys.UnknownError, string? resource = null, string? identifier = null, HttpStatusCode? status = null, Exception? inner = null) : Exception(AppErrors.Values[key], inner), IAppException {

	public string Key { get; init; } = key.ToString();

	public string? Resource { get; init; } = resource;

	public string? Identifier { get; init; } = identifier;

	public HttpStatusCode? StatusCode { get; init; } = status;

}
