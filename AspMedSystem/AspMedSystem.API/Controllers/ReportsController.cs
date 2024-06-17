﻿using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Commands.Reports;
using AspMedSystem.Application.UseCases.Queries.Reports;
using AspMedSystem.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspMedSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly UseCaseHandler handler;

        // GET: api/<ReportsController>
        public ReportsController(UseCaseHandler handler)
        {
            this.handler = handler;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] ReportSearchDTO dto, [FromServices] IReportSearchQuery query)
        {
            return Ok(handler.HandleQuery(query, dto));
        }

        // GET api/<ReportsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IReportSearchSingleQuery query)
        {
            return Ok(handler.HandleQuery(query, id));
        }

        // POST api/<ReportsController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] ReportCreateDTO dto, [FromServices] IReportCreateCommand command)
        {
            handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<ReportsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReportsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IReportDeleteCommand command)
        {
            handler.HandleCommand(command, id);
            return StatusCode(204);
        }
    }
}