using ECommerce.Errors;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
	[Route("errors/{code}")]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorsController : BaseApiController
	{
		public IActionResult Errors(int code)
		{
			return new ObjectResult(new ApiResponse(code));
		}
	}
}
