namespace Olympus.Core.Archend.Identity;

public record UserReadResponse : EntityReadResponse {

	public string Name { get; init; } = AppUsers.Unknown.Name;

	public string? Email { get; init; }

	public string? Title { get; init; }

	[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
	public UserPhotoLinkResponse? Photo { get; set; }

	public string? PhotoUrl => Photo is null ? null : AppUriBuilder.FromApi(CoreRoutes.Users.Photo).WithId(Id).WithCacheBusting(Photo.UpdatedAt).AsString();

}
