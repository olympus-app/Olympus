using Olympus.Architect.Domain.Entities;
using Olympus.Architect.Domain.Repositories;
using Olympus.Architect.Infrastructure.Database;

namespace Olympus.Architect.Infrastructure.Repositories;

public class PedidoRepository(DefaultDatabaseContext context) : IPedidoRepository {

	private readonly DefaultDatabaseContext _context = context;

	public async Task AddAsync(Pedido pedido) {

		await _context.Pedidos.AddAsync(pedido);

	}

}
