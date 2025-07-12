using Microsoft.EntityFrameworkCore;

namespace Olympus.Server.Database;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options) {

	protected override void OnModelCreating(ModelBuilder modelBuilder) {

		base.OnModelCreating(modelBuilder);

	}

}
