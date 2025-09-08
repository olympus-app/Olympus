namespace Olympus.Aether.Backend;

public class ExamplesTable : EntityTable<Example> {

	public override void Configure(EntityTypeBuilder<Example> builder) {

		// Base Configuration
		base.Configure(builder);

		// Columns Configuration
		builder.Property(e => e.Name).HasMaxLength(64);
	}

}
