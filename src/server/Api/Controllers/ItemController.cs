﻿
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
    public class ItemController : ControllerBase
    {
        [HttpPost]
        public IActionResult ItemClicked(
        [FromServices]SearchLearningService searchLearningService,
        string query,
        string id)
        {
            searchLearningService.ItemClicked(query, id);
            return Ok();
        }
    }
}
