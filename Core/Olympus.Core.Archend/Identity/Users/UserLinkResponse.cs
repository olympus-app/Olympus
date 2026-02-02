namespace Olympus.Core.Archend.Identity;

public record UserLinkResponse : EntityLinkResponse {

	public string Name { get; init; } = AppUsers.Unknown.Name;

	public string? Email { get; init; } = AppUsers.Unknown.Email;

	public string? Title { get; init; } = AppUsers.Unknown.Title;

	[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
	public UserPhotoLinkResponse? Photo { get; set; }

	public string? PhotoUrl => Photo is null ? null : AppUriBuilder.FromApi(CoreRoutes.Users.Photo).WithId(Id).WithCacheBusting(Photo.UpdatedAt).AsString();

}
