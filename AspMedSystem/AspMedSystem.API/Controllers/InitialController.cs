using AspMedSystem.Application.UseCases.Commands;
using AspMedSystem.Implementation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspMedSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitialController : ControllerBase
    {
        private UseCaseHandler _handler;

        public InitialController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<InitialController>
        [HttpGet]
        public IActionResult Get([FromServices] IDataInitializationCommand cmd)
        {
            _handler.HandleCommand(cmd, true);
            return Created();
        }

        // DELETE api/<InitialController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
