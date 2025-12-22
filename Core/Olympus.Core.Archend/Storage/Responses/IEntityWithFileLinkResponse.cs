namespace Olympus.Core.Archend.Storage;

public interface IEntityWithFileLinkResponse<TFileLink> : IEntityLinkResponse where TFileLink : class, IFileLinkResponse {

	public Guid EntityId { get; set; }

	public Guid FileId { get; set; }

	public TFileLink File { get; set; }

}
