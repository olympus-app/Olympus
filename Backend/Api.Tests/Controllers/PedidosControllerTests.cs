using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Olympus.Architect.Shared.DataObjects;

namespace Olympus.Backend.Api.Tests.Controllers;

public class PedidosControllerTests : IClassFixture<WebApplicationFactory<Program>> {

	private readonly HttpClient _client;

	public PedidosControllerTests(WebApplicationFactory<Program> factory) {

		_client = factory.CreateClient();

	}

	[Fact]
	public async Task Post_CriarPedido_DeveRetornarOkComDadosDoPedido() {

		// Arrange
		var novoPedidoDto = new CriarPedidoDto { Descricao = "Teste de Integração" };

		// Act
		var response = await _client.PostAsJsonAsync("/api/pedidos", novoPedidoDto);

		// Assert
		response.EnsureSuccessStatusCode();

		var pedidoRetornado = await response.Content.ReadFromJsonAsync<PedidoDto>();

		pedidoRetornado.Should().NotBeNull();
		pedidoRetornado.Id.Should().NotBeEmpty();
		pedidoRetornado.Descricao.Should().Be(novoPedidoDto.Descricao);

	}

}
