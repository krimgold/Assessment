using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Refactoring.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorController : ControllerBase
	{
		public IActionResult Error() => Problem();
	}
}
