using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Queries.AuditLogs;
using AspMedSystem.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspMedSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogsController : ControllerBase
    {
        private readonly UseCaseHandler handler;

        public AuditLogsController(UseCaseHandler handler)
        {
            this.handler = handler;
        }
        // GET: api/<AuditLogsController>
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] AuditLogSearchDTO dto, [FromServices] IAuditLogSearchQuery query)
        {
            return Ok(handler.HandleQuery(query, dto));
        }

        // GET api/<AuditLogsController>/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IAuditLogSearchSingleQuery query)
        {
            return Ok(handler.HandleQuery(query, id));
        }
    }
}
