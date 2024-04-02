using Exemplo.Infra.Singleton;
using Exemplo.Infra.Virtual;
using Microsoft.AspNetCore.Mvc;

namespace Exemplo.Singleton.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SingletonController : ControllerBase
    {
        private readonly SingletonContainer _singletonContainer;
        public SingletonController(SingletonContainer singletonContainer)
        {
            _singletonContainer = singletonContainer;
        }



        [HttpGet()]
        public IActionResult Get()
        {
            //var singleton = SingletonVirtual.Instance;
            return Ok(_singletonContainer);
        }


        //public SingletonContainer SingletonContainer { get; }

        //// GET api/v1/<VeiculosController>/5
        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    return id switch
        //    {
        //        1 => Ok(SingletonContainer),
        //        _ => Ok(Singleton.Instance)
        //    };
        //}



    }
}
