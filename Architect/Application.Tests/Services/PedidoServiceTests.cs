using Moq;
using FluentAssertions;
using Olympus.Architect.Application.Interfaces;
using Olympus.Architect.Application.Services;
using Olympus.Architect.Domain.Entities;
using Olympus.Architect.Domain.Repositories;
using Olympus.Architect.Shared.DataObjects;

namespace Olympus.Architect.Application.Tests.Services;

public class PedidoServiceTests {

	private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
	private readonly Mock<IUnitOfWork> _unitOfWorkMock;
	private readonly IPedidoService _pedidoAppService;

	public PedidoServiceTests() {

		_pedidoRepositoryMock = new Mock<IPedidoRepository>();
		_unitOfWorkMock = new Mock<IUnitOfWork>();

		_pedidoAppService = new PedidoService(
				_pedidoRepositoryMock.Object,
				_unitOfWorkMock.Object);
	}

	[Fact]
	public async Task CriarPedidoAsync_DeveCriarEChamarSaveChanges_QuandoDadosValidos() {

		// Arrange
		var requestDto = new CriarPedidoDto { Descricao = "Pedido de Teste" };

		// Act
		var resultadoDto = await _pedidoAppService.CriarPedidoAsync(requestDto);

		// Assert
		resultadoDto.Should().NotBeNull();
		resultadoDto.Id.Should().NotBeEmpty();
		resultadoDto.Descricao.Should().Be(requestDto.Descricao);

		// Verifica se os métodos das dependências foram chamados como esperado
		_pedidoRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Pedido>()), Times.Once);
		_unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

	}

}
