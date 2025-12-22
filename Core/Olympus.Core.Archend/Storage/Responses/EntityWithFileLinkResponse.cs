namespace Olympus.Core.Archend.Storage;

public abstract record EntityWithFileLinkResponse<TFileLink> : EntityLinkResponse, IEntityWithFileLinkResponse<TFileLink> where TFileLink : class, IFileLinkResponse {

	public Guid EntityId { get; set; }

	public Guid FileId { get; set; }

	public TFileLink File { get; set; } = null!;

}
