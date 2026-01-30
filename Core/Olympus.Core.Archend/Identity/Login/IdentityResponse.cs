namespace Olympus.Core.Archend.Identity;

public record IdentityResponse : IResponse {

	public Guid Id { get; init; }

	public string Name { get; init; } = AppUsers.Unknown.Name;

	public string? UserName { get; init; }

	public string? Email { get; init; }

	public string? Title { get; init; }

	public string? Photo { get; init; }

	public List<string> Roles { get; init; } = [];

	public string? Permissions { get; init; }

}
