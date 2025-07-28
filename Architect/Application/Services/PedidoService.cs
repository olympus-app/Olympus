using Olympus.Architect.Application.Interfaces;
using Olympus.Architect.Domain.Entities;
using Olympus.Architect.Domain.Repositories;
using Olympus.Architect.Shared.DataObjects;

namespace Olympus.Architect.Application.Services;

public class PedidoService(IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork) : IPedidoService {

	private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	public async Task<PedidoDto> CriarPedidoAsync(CriarPedidoDto dto) {

		var novoPedido = new Pedido(dto.Descricao);

		await _pedidoRepository.AddAsync(novoPedido);

		await _unitOfWork.SaveChangesAsync();

		return new PedidoDto {

			Id = novoPedido.Id,
			Descricao = novoPedido.Descricao

		};

	}

}
