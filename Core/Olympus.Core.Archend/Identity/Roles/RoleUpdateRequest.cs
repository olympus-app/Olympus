namespace Olympus.Core.Archend.Identity;

public record RoleUpdateRequest : EntityUpdateRequest {

	public required string Name { get; set; }

}
