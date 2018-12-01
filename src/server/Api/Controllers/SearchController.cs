using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.SearchLearning;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IActionResult Search([FromServices]ElasticService elasticService, String query)
        {
            return Ok(elasticService.Search(query));
        }
    }
}
