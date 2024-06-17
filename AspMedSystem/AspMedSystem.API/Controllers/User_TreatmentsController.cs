using AspMedSystem.Implementation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspMedSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_TreatmentsController : ControllerBase
    {
        private readonly UseCaseHandler handler;

        // GET: api/<User_TreatmentsController>
        public User_TreatmentsController(UseCaseHandler handler)
        {
            this.handler = handler;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<User_TreatmentsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<User_TreatmentsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<User_TreatmentsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<User_TreatmentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
