using System;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class JavascriptController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromServices]JavascriptExecutor javascriptExecutor)
        {
            return Ok(javascriptExecutor.Execute(new { parameter = "asd" }));
        }
    }
}