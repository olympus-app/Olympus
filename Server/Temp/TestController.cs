using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Olympus.Server.Authentication;

namespace Olympus.Server.Temp;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase {

	private readonly ICurrentUserService _currentUserService;

	public TestController(ICurrentUserService currentUserService) {

		_currentUserService = currentUserService;

	}

	// Public endpoint, accessible by anyone
	[HttpGet("public")]
	[AllowAnonymous]
	public IActionResult GetPublic() {

		return Ok(new { Message = "This is a public route, anyone can access it." });

	}

	// Protected endpoint, requires authentication
	[HttpGet("protected")]
	[Authorize]
	public IActionResult GetProtected() {

		var olympusUserId = _currentUserService.UserId;

		return Ok(new {
			Message = "This is a protected route. You are authenticated!",
			InternalOlympusId = olympusUserId
		});

	}

}
