namespace Olympus.Core.Archend.Identity;

public record RoleReadResponse : EntityReadResponse {

	public string Name { get; set; } = string.Empty;

}
