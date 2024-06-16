using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Commands.Examinations;
using AspMedSystem.Application.UseCases.Queries.Examinations;
using AspMedSystem.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspMedSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationsController : ControllerBase
    {
        private readonly UseCaseHandler handler;

        public ExaminationsController(UseCaseHandler handler)
        {
            this.handler = handler;
        }
        // GET: api/<ExaminationsController>
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] ExaminationSearchDTO search, [FromServices] IExaminationSearchQuery query)
        {
            return Ok(handler.HandleQuery(query, search));
        }

        // GET api/<ExaminationsController>/5
        [Authorize]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ExaminationsController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] ExaminationCreateDTO dto, [FromServices] IExaminationCreateCommand command)
        {
            handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [Authorize]
        [HttpPatch("{id}")]
        public IActionResult MarkPerformed(int id, [FromServices] IExaminationPerformedCommand command)
        {
            handler.HandleCommand(command, id);
            return StatusCode(204);
        }

        // DELETE api/<ExaminationsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
