namespace Olympus.Core.Archend.Identity;

public record RoleListResponse : EntityListResponse {

	public string Name { get; set; } = string.Empty;

}
