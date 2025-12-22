namespace Olympus.Core.Archend.Identity;

public record UserListResponse : EntityListResponse {

	public string Name { get; init; } = AppUsers.Unknown.Name;

	public string? Email { get; init; }

	public string? Title { get; init; }

	[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
	public UserPhotoLinkResponse? UserPhoto { get; set; }

	public string? Photo => UserPhoto?.GetPhotoUrl();

}
