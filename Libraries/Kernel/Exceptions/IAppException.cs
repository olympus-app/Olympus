using System.Net;

namespace Olympus.Kernel;

public interface IAppException {

	public string Key { get; }

	public string? Resource { get; }

	public string? Identifier { get; }

	public HttpStatusCode? StatusCode { get; }

}
