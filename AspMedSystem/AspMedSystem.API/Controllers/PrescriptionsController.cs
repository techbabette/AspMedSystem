using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Commands.UserTreatments;
using AspMedSystem.Application.UseCases.Queries.Examinations;
using AspMedSystem.Application.UseCases.Queries.UserTreatments;
using AspMedSystem.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspMedSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly UseCaseHandler handler;

        // GET: api/<User_TreatmentsController>
        public PrescriptionsController(UseCaseHandler handler)
        {
            this.handler = handler;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] UserTreatmentSearchDTO search, [FromServices] IUserTreatmentSearchQuery query)
        {
            return Ok(handler.HandleQuery(query, search));
        }

        // GET api/<ExaminationsController>/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IExaminationSearchSingleOthersQuery query)
        {
            return Ok(handler.HandleQuery(query, id));
        }

        [Authorize]
        [HttpGet("me/prescribee")]
        public IActionResult GetAsExaminee([FromQuery] UserTreatmentSearchDTO search, [FromServices] IUserTreatmentSearchPrescribeeQuery query)
        {
            return Ok(handler.HandleQuery(query, search));
        }

        [Authorize]
        [HttpGet("me/prescriber")]
        public IActionResult GetAsExaminer([FromQuery] UserTreatmentSearchDTO search, [FromServices] IUserTreatmentSearchPrescriberQuery query)
        {
            return Ok(handler.HandleQuery(query, search));
        }

        [Authorize]
        [HttpGet("me/prescribee/{id}")]
        public IActionResult GetOneAsExaminee(int id, [FromServices] IUserTreatmentSearchSinglePrescribeeQuery query)
        {
            return Ok(handler.HandleQuery(query, id));
        }

        [Authorize]
        [HttpGet("me/prescriber/{id}")]
        public IActionResult GetOneAsExaminer(int id, [FromServices] IUserTreatmentSearchSinglePrescriberQuery query)
        {
            return Ok(handler.HandleQuery(query, id));
        }

        // POST api/<User_TreatmentsController>
        [HttpPost]
        public IActionResult Post([FromBody] UserTreatmentCreateDTO dto, [FromServices] IUserTreatmentCreateCommand command)
        {
            handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<User_TreatmentsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserTreatmentUpdateDTO dto, [FromServices] IUserTreatmentUpdateCommand command)
        {
            dto.Id = id;
            handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        // DELETE api/<User_TreatmentsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IUserTreatmentDeleteCommand command)
        {
            handler.HandleCommand(command, id);
            return StatusCode(204);
        }
    }
}
