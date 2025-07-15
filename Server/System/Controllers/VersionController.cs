using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Olympus.Server.Statics;

namespace Olympus.Server.System;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class SystemVersionController : ControllerBase {

	[HttpGet]
	public IActionResult GetVersion() {

		var result = new {
			build = SystemVars.SystemBuild,
			version = SystemVars.SystemVersion
		};

		return Ok(result);

	}

}
