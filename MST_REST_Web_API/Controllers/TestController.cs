using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MST_REST_Web_API.Models.DTO;
using MST_REST_Web_API.Services;

namespace MST_REST_Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly ITestService _testService;
        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Configurator,Admin")]
        public ActionResult AddTest([FromBody] ScriptDto dto)
        {
            _testService.AddTest(dto);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Configurator,Admin")]
        public ActionResult DeleteTest([FromRoute] int id)
        {
            _testService.DeleteTest(id);
            return NoContent();
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "Configurator,Admin")]
        public ActionResult UpdateTest([FromRoute] int id, [FromBody] ScriptDto dto)
        {
            _testService.UpdateTest(id, dto);
            return Ok();
        }

        [HttpPatch("execute/{id}")]
        [Authorize(Roles = "Tester")]
        public ActionResult ExecuteTest([FromRoute] int id, [FromBody] TestDto dto)
        {
            _testService.ExecuteTest(id, dto);
            return Ok();
        }

        [HttpGet("gettodo")]
        [Authorize(Roles = "Tester")]
        public ActionResult GetToDo()
        {
            var result =  _testService.GetToDo();
            return Ok(result);
        }

        [HttpGet("getall")]
        [Authorize(Roles = "Configurator,Admin")]
        public ActionResult GetAll()
        {
            var result = _testService.GetAll();
            return Ok(result);
        }


    }
}
