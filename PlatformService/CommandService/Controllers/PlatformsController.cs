using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/commands/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {

        }

        [HttpPost]
        public async Task<IActionResult> TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST # Command Service");
            return Ok();
        }
    }
}
