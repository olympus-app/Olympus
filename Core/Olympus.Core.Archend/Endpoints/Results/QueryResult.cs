#pragma warning disable OL0007

namespace Olympus.Core.Archend.Endpoints;

public class QueryResult<T>(int page, int count, int totalPages, int totalCount, IEnumerable<T> items) : IEntityResponse where T : class {

	public QueryResult() : this(0, 0, 0, 0, []) { }

	[JsonPropertyOrder(0)]
	public int Page { get; init; } = page;

	[JsonPropertyOrder(1)]
	public int Count { get; init; } = count;

	[JsonPropertyOrder(2)]
	public int TotalPages { get; init; } = totalPages;

	[JsonPropertyOrder(3)]
	public int TotalCount { get; init; } = totalCount;

	[JsonPropertyOrder(4)]
	public IEnumerable<T> Items { get; init; } = items;

	public void Deconstruct(out int page, out int pages, out int count, out int total, out IEnumerable<T> items) {

		page = Page;
		count = Count;
		pages = TotalPages;
		total = TotalCount;
		items = Items;

	}

}

#pragma warning restore OL0007
