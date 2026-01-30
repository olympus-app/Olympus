namespace Olympus.Core.Archend.Identity;

public record PasskeyUnregisterRequest : IRequest {

	[QueryParam(IsRequired = true)]
	public required string Key { get; set; }

}
