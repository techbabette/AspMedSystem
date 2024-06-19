using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Commands.ExaminationTerms;
using AspMedSystem.Application.UseCases.Queries.ExaminationTerms;
using AspMedSystem.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspMedSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Examination_termsController : ControllerBase
    {
        private readonly UseCaseHandler handler;

        public Examination_termsController(UseCaseHandler handler)
        {
            this.handler = handler;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] ExaminationTermSearchDTO dto, [FromServices] IExaminationTermSearchQuery query)
        {
            return Ok(handler.HandleQuery(query, dto));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] ExaminationTermCreateDTO dto, [FromServices] IExaminationTermCreateCommand command)
        {
            handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ExaminationTermUpdateDTO dto, [FromServices] IExaminationTermUpdateCommand command)
        {
            dto.Id = id;
            handler.HandleCommand(command, dto); ;
            return StatusCode(204);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IExaminationTermDeleteCommand command)
        {
            handler.HandleCommand(command, id);
            return StatusCode(204);
        }
    }
}
