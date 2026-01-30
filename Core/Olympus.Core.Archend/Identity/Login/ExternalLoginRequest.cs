namespace Olympus.Core.Archend.Identity;

public record ExternalLoginRequest : IRequest {

	[QueryParam(IsRequired = true)]
	public required string Provider { get; set; }

	[QueryParam(IsRequired = false)]
	public string? ReturnUrl { get; set; }

}
