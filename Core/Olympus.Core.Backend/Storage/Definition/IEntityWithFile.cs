namespace Olympus.Core.Backend.Storage;

public interface IEntityWithFile<TEntity, TFile> : IEntity where TEntity : class, IEntity where TFile : class, IFileEntity {

	public Guid EntityId { get; set; }

	public TEntity Entity { get; set; }

	public Guid FileId { get; set; }

	public TFile File { get; set; }

}
