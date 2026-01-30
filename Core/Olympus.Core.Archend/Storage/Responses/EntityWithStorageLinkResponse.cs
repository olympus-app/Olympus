namespace Olympus.Core.Archend.Storage;

public abstract record EntityWithStorageLinkResponse<TStorageEntityLink> : EntityLinkResponse, IEntityWithStorageLinkResponse<TStorageEntityLink> where TStorageEntityLink : class, IStorageEntityLinkResponse {

	public Guid FileId { get; set; }

	public TStorageEntityLink File { get; set; } = null!;

}
