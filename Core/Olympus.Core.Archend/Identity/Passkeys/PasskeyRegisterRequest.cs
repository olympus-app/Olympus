namespace Olympus.Core.Archend.Identity;

public record PasskeyRegisterRequest {

	public required string Credential { get; init; }

	public string? Error { get; set; }

}
