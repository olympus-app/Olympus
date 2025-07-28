using Olympus.Architect.Domain.Entities;

namespace Olympus.Architect.Domain.Repositories;

public interface IPedidoRepository {

	public Task AddAsync(Pedido pedido);

}
