namespace Olympus.Core.Archend.Identity;

public record RoleQueryResponse : EntityQueryResponse {

	public string Name { get; set; } = string.Empty;

}
