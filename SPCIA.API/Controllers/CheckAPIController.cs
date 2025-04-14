using Microsoft.AspNetCore.Mvc;

namespace SPCIA.API.Controllers
{
    public class CheckAPIController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet("/health")]
        public IActionResult Health()
        {
            return Ok();
        }
    }
}
