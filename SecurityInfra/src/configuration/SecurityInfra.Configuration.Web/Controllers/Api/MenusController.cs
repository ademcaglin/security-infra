using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SecurityInfra.Configuration.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class MenusController : Controller
    {
        [HttpGet("{id}")]
        public IEnumerable<string> Get(string id)
        {
            return new string[] { "value1", "value2" };
        }
    }
}
