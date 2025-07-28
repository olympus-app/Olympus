using Microsoft.EntityFrameworkCore;
using Olympus.Architect.Domain.Entities;

namespace Olympus.Architect.Infrastructure.Database;

public class DefaultDatabaseContext : DbContext {

	public DefaultDatabaseContext(DbContextOptions<DefaultDatabaseContext> options) : base(options) { }

	public DbSet<Pedido> Pedidos { get; set; }

}
