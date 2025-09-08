namespace Olympus.Aether.Archend;

public partial class Example : Entity {

	public override required Guid Id { get; set; } = Guid.CreateVersion7();

	public required string Name { get; set; }

}
