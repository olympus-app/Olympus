namespace Olympus.Core.Archend.Identity;

public record AntiforgeryResponse {

	public string? Header { get; set; } = Headers.Antiforgery;

	public string? Token { get; set; } = string.Empty;

}
