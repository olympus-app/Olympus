namespace Olympus.Core.Backend.Storage;

public record FileLinkConfiguration : IFileLinkConfiguration {

	public virtual int Expiration { get; set; } = 3600;

}
