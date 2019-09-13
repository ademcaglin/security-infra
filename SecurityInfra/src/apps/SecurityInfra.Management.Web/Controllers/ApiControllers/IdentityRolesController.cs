using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SecurityInfra.Management.Web.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class IdentityRolesController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            // user tenant için admin rolüne sahip mi? 
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}/rules")]
        public IActionResult Get(int id)
        {
            return Json(new
            {
                RequiredResourceType = ""
            });
        }
    }
}
