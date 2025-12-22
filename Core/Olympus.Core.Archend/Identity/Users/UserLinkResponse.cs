namespace Olympus.Core.Archend.Identity;

public record UserLinkResponse : EntityLinkResponse {

	public string Name { get; init; } = AppUsers.Unknown.Name;

	public string? Email { get; init; } = AppUsers.Unknown.Email;

	public string? Title { get; init; } = AppUsers.Unknown.Title;

}
