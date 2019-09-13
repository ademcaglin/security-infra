using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecurityInfra.Configuration.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class ClaimsController : Controller
    {
        [HttpGet("{id}")]
        public IEnumerable<string> Get(string id, string tenantId)
        {
            return new string[] { "value1", "value2" };
        }
    }
}
