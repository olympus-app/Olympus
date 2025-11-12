namespace Olympus.Core.Backend.Databases;

public class EntitiesDatabase(DbContextOptions<EntitiesDatabase> options, IEnumerable<IEntityTable> tables) : DbContext(options) {

	protected override void OnModelCreating(ModelBuilder modelBuilder) {

		base.OnModelCreating(modelBuilder);

		foreach (var table in tables) table.Apply(modelBuilder);

	}

}
