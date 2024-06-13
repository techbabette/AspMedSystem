using AspMedSystem.Implementation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspMedSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UseCasesController : ControllerBase
    {
        // GET: api/<UseCasesController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(UseCaseInfo.AllUseCases);
        }

        // GET api/<UseCasesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UseCasesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UseCasesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UseCasesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
