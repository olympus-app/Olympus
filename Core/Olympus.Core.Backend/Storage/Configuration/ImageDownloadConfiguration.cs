namespace Olympus.Core.Backend.Storage;

public record ImageDownloadConfiguration : FileDownloadConfiguration {

	public override string CacheControl { get; set; } = "private, max-age=3600";

}
