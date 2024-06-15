using AspMedSystem.Application.UseCases.Queries.Users;
using AspMedSystem.Implementation;
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

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("me/information")]
        public IActionResult GetSelfInformation([FromServices] IUserSearchSelfInformationQuery query)
        {
            return Ok(_handler.HandleQuery(query, true));
        }

        [HttpGet("{id}/information")]
        public IActionResult GetUserInformation(int id, [FromServices] IUserSearchSingleInformationQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

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

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
