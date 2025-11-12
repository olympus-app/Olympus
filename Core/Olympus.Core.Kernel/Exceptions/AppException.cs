namespace Olympus.Core.Kernel.Exceptions;

public abstract class AppException(AppErrors.Keys key = AppErrors.Keys.UnknownError, string? resource = null, string? identifier = null, Exception? inner = null) : Exception(AppErrors.Values[key], inner), IAppException {

	public string Localizer { get; init; } = key.ToString();

	public string? Resource { get; init; } = resource;

	public string? Identifier { get; init; } = identifier;

}
