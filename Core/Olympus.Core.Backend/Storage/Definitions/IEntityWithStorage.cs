namespace Olympus.Core.Backend.Storage;

public interface IEntityWithStorage<TStorageEntity> : IEntity where TStorageEntity : class, IStorageEntity {

	public Guid FileId { get; set; }

	public TStorageEntity File { get; set; }

}
