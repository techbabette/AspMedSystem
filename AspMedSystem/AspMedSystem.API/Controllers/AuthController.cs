using AspMedSystem.API.Core;
using AspMedSystem.API.DTO;
using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Commands;
using AspMedSystem.DataAccess;
using AspMedSystem.Implementation;
using Azure.Core;
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
        private readonly JwtTokenCreator _tokenCreator;
        public AuthController(UseCaseHandler handler, JwtTokenCreator tokenCreator)
        {
            _useCaseHandler = handler;
            _tokenCreator = tokenCreator;
        }

        // POST api/<AuthController>
        [HttpPost("register")]
        public IActionResult Post([FromBody] AuthRegisterDTO dto, [FromServices] IAuthRegisterCommand cmd)
        {
            _useCaseHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public IActionResult Post([FromBody] AuthLoginRequest request)
        {
            string token = _tokenCreator.Create(request.Email, request.Password);

            return Ok(new AuthResponse { Token = token });
        }
    }
}
