namespace Olympus.Core.Backend.Storage;

public record FileDownloadConfiguration : IFileDownloadConfiguration {

	public virtual string CacheControl { get; set; } = string.Empty;

}
