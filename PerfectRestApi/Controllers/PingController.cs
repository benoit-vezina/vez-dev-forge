using Microsoft.AspNetCore.Mvc;

namespace PerfectRestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PingController(ILogger<PingController> logger) : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Ping()
        {
            logger.LogInformation("Ping request received");

            return Ok("Pong");
        }
    }
}
