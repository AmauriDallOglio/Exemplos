using Exemplo.Infra.Singleton;
using Exemplo.Infra.Virtual;
using Microsoft.AspNetCore.Mvc;

namespace Exemplo.Singleton.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ScopedController : ControllerBase
    {


        [HttpGet()]
        public IActionResult Get()
        {
            var singleton = new ScopedVirtual();
            return Ok(singleton);
        }
    }
}
