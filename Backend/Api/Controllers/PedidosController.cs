using Microsoft.AspNetCore.Mvc;
using Olympus.Architect.Application.Services;
using Olympus.Architect.Shared.DataObjects;

namespace Olympus.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController(IPedidoService pedidoAppService) : ControllerBase {

	private readonly IPedidoService _pedidoAppService = pedidoAppService;

	[HttpPost]
	public async Task<IActionResult> Criar([FromBody] CriarPedidoDto dto) {

		var pedidoDto = await _pedidoAppService.CriarPedidoAsync(dto);
		return Ok(pedidoDto);

	}

}
