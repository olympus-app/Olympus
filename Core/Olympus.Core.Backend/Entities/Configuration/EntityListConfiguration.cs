namespace Olympus.Core.Backend.Entities;

public record EntityListConfiguration {

	public virtual string CacheControl { get; set; } = ResponseCache.From(CachePolicy.Private, 1.Days());

}
