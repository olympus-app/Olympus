namespace Olympus.Core.Archend.Endpoints;

public class PageResult<TResponse>(int page, int pageSize, IEnumerable<TResponse> items, int totalCount) : IResponse where TResponse : class, IResponse {

	[JsonPropertyOrder(0)]
	public int Page { get; init; } = page;

	[JsonPropertyOrder(1)]
	public int Count { get; init; } = items.Count();

	[JsonPropertyOrder(2)]
	public int TotalPages { get; init; } = (int)Math.Ceiling((double)totalCount / (pageSize > 0 ? pageSize : 1));

	[JsonPropertyOrder(3)]
	public int TotalCount { get; init; } = totalCount;

	[JsonPropertyOrder(4)]
	public IEnumerable<TResponse> Items { get; init; } = items;

	public void Deconstruct(out int page, out int pages, out int count, out int total, out IEnumerable<TResponse> items) {

		page = Page;
		count = Count;
		pages = TotalPages;
		total = TotalCount;
		items = Items;

	}

}
