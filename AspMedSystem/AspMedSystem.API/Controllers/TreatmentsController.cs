using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Commands.Treatments;
using AspMedSystem.Application.UseCases.Queries.Treatments;
using AspMedSystem.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspMedSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentsController : ControllerBase
    {
        private readonly UseCaseHandler handler;

        public TreatmentsController(UseCaseHandler handler)
        {
            this.handler = handler;
        }
        // GET: api/<TreatmentsController>
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] TreatmentSearchDTO dto, [FromServices] ITreatmentSearchQuery query)
        {
            return Ok(handler.HandleQuery(query, dto));
        }

        // GET api/<TreatmentsController>/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] ITreatmentSearchSingleQuery query)
        {
            return Ok(handler.HandleQuery(query, id));
        }

        // POST api/<TreatmentsController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] TreatmentCreateDTO dto, [FromServices] ITreatmentCreateCommand command)
        {
            handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<TreatmentsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TreatmentUpdateDTO dto, [FromServices] ITreatmentUpdateCommand command)
        {
            dto.Id = id;
            handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        // DELETE api/<TreatmentsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] ITreatmentDeleteCommand command)
        {
            handler.HandleCommand(command, id);
            return StatusCode(204);
        }
    }
}
