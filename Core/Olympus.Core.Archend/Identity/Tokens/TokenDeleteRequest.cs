namespace Olympus.Core.Archend.Identity;

public record TokenDeleteRequest : EntityDeleteRequest {

	[QueryParam(IsRequired = true)]
	public Guid Key { get; set; }

}
