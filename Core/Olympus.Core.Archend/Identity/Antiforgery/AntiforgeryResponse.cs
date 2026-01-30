namespace Olympus.Core.Archend.Identity;

public record AntiforgeryResponse : IResponse {

	public string? Header { get; set; } = HttpHeaders.Antiforgery;

	public string? Token { get; set; } = string.Empty;

}
