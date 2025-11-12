namespace Olympus.Core.Kernel.Exceptions;

public interface IAppException {

	public string Localizer { get; }

	public string? Resource { get; }

	public string? Identifier { get; }

}
