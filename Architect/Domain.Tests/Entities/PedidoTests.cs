using FluentAssertions;
using Olympus.Architect.Domain.Entities;

namespace Olympus.Architect.Domain.Tests.Entities;

public class PedidoTests {

	[Fact]
	public void Construtor_DeveCriarPedido_ComIdValidoEDescricaoCorreta() {

		// Arrange (Organizar)
		var descricao = "Meu primeiro pedido";

		// Act (Agir)
		var pedido = new Pedido(descricao);

		// Assert (Verificar)
		pedido.Should().NotBeNull();
		pedido.Id.Should().NotBe(Guid.Empty);
		pedido.Descricao.Should().Be(descricao);

	}

}
