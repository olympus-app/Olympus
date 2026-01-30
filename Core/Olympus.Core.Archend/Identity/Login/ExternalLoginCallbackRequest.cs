namespace Olympus.Core.Archend.Identity;

public record ExternalLoginCallbackRequest : IRequest {

	[QueryParam(IsRequired = false)]
	public string? ReturnUrl { get; set; }

	[QueryParam(IsRequired = false)]
	public string? RemoteError { get; set; }

}
