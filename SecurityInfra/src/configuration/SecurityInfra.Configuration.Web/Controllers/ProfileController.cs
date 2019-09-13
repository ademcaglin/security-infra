using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SecurityInfra.Configuration.Web.Controllers
{
    [Route("[controller]")]
    public class ProfileController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(User.Claims.Select(x => new
            {
                x.Type,
                x.Value
            }));
        }
    }
}
