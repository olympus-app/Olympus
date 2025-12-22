#pragma warning disable OL0007

namespace Olympus.Core.Archend.Endpoints;

public class ListResult<T>(int count, IEnumerable<T> items) : IEntityResponse where T : class {

	public ListResult() : this(0, []) { }

	[JsonPropertyOrder(0)]
	public int Count { get; init; } = count;

	[JsonPropertyOrder(4)]
	public IEnumerable<T> Items { get; init; } = items;

	public void Deconstruct(out int count, out IEnumerable<T> items) {

		count = Count;
		items = Items;

	}

}

#pragma warning restore OL0007
