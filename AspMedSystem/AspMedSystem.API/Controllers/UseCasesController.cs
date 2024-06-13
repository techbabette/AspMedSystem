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
    }
}
