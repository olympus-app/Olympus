namespace Olympus.Core.Archend.Endpoints;

public class ListResult<TResponse>(IEnumerable<TResponse> items) : IResponse where TResponse : class, IResponse {

	[JsonPropertyOrder(0)]
	public int Count { get; private set; } = items.Count();

	[JsonPropertyOrder(1)]
	public IEnumerable<TResponse> Items { get; init; } = items;

	public void Deconstruct(out int count, out IEnumerable<TResponse> items) {

		count = Count;
		items = Items;

	}

}
