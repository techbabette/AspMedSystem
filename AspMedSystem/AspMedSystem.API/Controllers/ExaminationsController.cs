using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Commands.Examinations;
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
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ExaminationsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ExaminationsController>
        [Authorize]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [Authorize]
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
