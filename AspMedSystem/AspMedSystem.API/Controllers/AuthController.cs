using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Commands;
using AspMedSystem.DataAccess;
using AspMedSystem.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspMedSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;
        public AuthController(UseCaseHandler handler)
        {
            _useCaseHandler = handler;
        }

        // POST api/<AuthController>
        [HttpPost("register")]
        public IActionResult Post([FromBody] AuthRegisterDTO dto, [FromServices] IAuthRegisterCommand cmd)
        {
            _useCaseHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
