namespace Olympus.Core.Archend.Identity;

public record RoleCreateRequest : EntityCreateRequest {

	public required string Name { get; set; }

}
