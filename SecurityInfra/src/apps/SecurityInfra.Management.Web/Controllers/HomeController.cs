using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecurityInfra.Management.Web.Controllers
{
    // only admins
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if(Request.Cookies["CurrentPage"].ToString() == null)
            {
                // get first role from user claims check page
                // set cookie, then redirect to page
                // tenantadmin/123456 || systemadmin || departmentadmin/123456
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Change(string page, string pageId)
        {
            if (string.IsNullOrEmpty(page))
            {

            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile()
        {
            return null;
        }
    }
}
