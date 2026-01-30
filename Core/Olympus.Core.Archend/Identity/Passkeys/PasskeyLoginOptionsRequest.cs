namespace Olympus.Core.Archend.Identity;

public record PasskeyLoginOptionsRequest : IRequest {

	[QueryParam(IsRequired = false)]
	public string? Username { get; set; }

}
