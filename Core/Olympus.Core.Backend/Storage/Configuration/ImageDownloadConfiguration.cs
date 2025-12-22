namespace Olympus.Core.Backend.Storage;

public record ImageDownloadConfiguration : FileDownloadConfiguration {

	public override string CacheControl { get; set; } = ResponseCache.From(CachePolicy.Private, 1.Days());

}
