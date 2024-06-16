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
        // GET: api/<Examination_termsController>
        [HttpGet]
        public IActionResult Get([FromQuery] ExaminationTermSearchDTO dto, [FromServices] IExaminationTermSearchQuery query)
        {
            return Ok(handler.HandleQuery(query, dto));
        }

        // GET api/<Examination_termsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Examination_termsController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] ExaminationTermCreateDTO dto, [FromServices] IExaminationTermCreateCommand command)
        {
            handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<Examination_termsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Examination_termsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
