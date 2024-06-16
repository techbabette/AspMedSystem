using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Commands.Users;
using AspMedSystem.Application.UseCases.Queries.Users;
using AspMedSystem.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspMedSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        UseCaseHandler _handler;
        public UsersController([FromServices] UseCaseHandler handler)
        {
            _handler = handler;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get([FromServices] IUserSearchQuery query, [FromQuery] UserSearchDTO search)
        {
            return Ok(_handler.HandleQuery(query, search));
        }
        [Authorize]
        [HttpGet("me/information")]
        public IActionResult GetSelfInformation([FromServices] IUserSearchSelfInformationQuery query)
        {
            return Ok(_handler.HandleQuery(query, true));
        }

        [Authorize]
        [HttpGet("{id}/information")]
        public IActionResult GetUserInformation(int id, [FromServices] IUserSearchSingleInformationQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        [Authorize]
        [HttpGet("me/permissions")]
        public IActionResult GetSelfPermissions([FromServices] IUserSearchSelfPermissionsQuery query)
        {
            return Ok(_handler.HandleQuery(query, true));
        }

        [HttpGet("{id}/permissions")]
        public IActionResult GetUserPermissions(int id, [FromServices] IUserSearchSinglePermissionsQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        [Authorize]
        [HttpPut("me/information")]
        public IActionResult PutSelfInformation([FromBody] UserUpdateInformationDTO dto, [FromServices] IUserUpdateInformationSelfCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        // PUT api/<UsersController>/5
        [Authorize]
        [HttpPut("{id}/information")]
        public IActionResult PutOthersInformation(int id, [FromBody] UserUpdateInformationDTO dto, [FromServices] IUserUpdateInformationOthersCommand command)
        {
            dto.Id = id;
            _handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        [Authorize]
        [HttpPut("{id}/permissions")]
        public IActionResult PutPermissions(int id, [FromBody] UserUpdatePermissionsDTO dto, [FromServices] IUserUpdatePermissionsCommand command)
        {
            dto.Id = id;
            _handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        // DELETE api/<UsersController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IUserDeleteCommand command)
        {
            _handler.HandleCommand(command, id);
            return StatusCode(204);
        }
    }
}
