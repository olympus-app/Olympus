using Olympus.Architect.Shared.DataObjects;

namespace Olympus.Architect.Application.Services;

public interface IPedidoService {

	Task<PedidoDto> CriarPedidoAsync(CriarPedidoDto dto);

}
