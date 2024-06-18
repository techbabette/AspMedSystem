using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Queries.Examiners;
using AspMedSystem.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspMedSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminersController : ControllerBase
    {
        private readonly UseCaseHandler handler;

        public ExaminersController(UseCaseHandler handler)
        {
            this.handler = handler;
        }
        // GET: api/<ExaminersController>
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] ExaminerSearchDTO dto, [FromServices] IExaminerSearchQuery query)
        {
            return Ok(handler.HandleQuery(query, dto));
        }

        // GET api/<ExaminersController>/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IExaminerSearchSingleQuery query)
        {
            return Ok(handler.HandleQuery(query, id));
        }
    }
}
