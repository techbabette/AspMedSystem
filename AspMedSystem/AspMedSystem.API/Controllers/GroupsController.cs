using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Commands.Groups;
using AspMedSystem.Application.UseCases.Queries.Groups;
using AspMedSystem.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspMedSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        // GET: api/<GroupController>
        private UseCaseHandler _handler;
        public GroupsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] GroupSearchDTO dto, [FromServices] IGroupSearchQuery query)
        {
            return Ok(_handler.HandleQuery(query, dto));
        }

        // GET api/<GroupController>/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGroupSearchSingleQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] GroupCreateDTO dto, [FromServices] IGroupCreateCommand cmd)
        {
            _handler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        // PUT api/<GroupController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] GroupUpdateDTO dto, [FromServices] IGroupUpdateCommand cmd)
        {
            dto.Id = id;
            _handler.HandleCommand(cmd, dto);
            return StatusCode(204);
        }

        // DELETE api/<GroupController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IGroupDeleteCommand cmd)
        {
            _handler.HandleCommand(cmd, id);
            return StatusCode(204);
        }
    }
}
