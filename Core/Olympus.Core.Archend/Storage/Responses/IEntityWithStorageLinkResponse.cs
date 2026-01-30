namespace Olympus.Core.Archend.Storage;

public interface IEntityWithStorageLinkResponse<TStorageEntityLinkResponse> : IEntityLinkResponse where TStorageEntityLinkResponse : class, IStorageEntityLinkResponse {

	public Guid FileId { get; set; }

	public TStorageEntityLinkResponse File { get; set; }

}
