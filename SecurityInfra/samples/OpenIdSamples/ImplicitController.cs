using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using IdentityModel.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Globalization;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UdhbGiris.Samples
{
    public class ImplicitController : Controller
    {
        [Authorize]
        public IActionResult Secure()
        {
            var content = "<html><body><p>UDHB Tek Oturum Açma</p> <a href='/account/logout'>Oturumu Kapat</a> </body></html>";

            return new ContentResult()
            {
                Content = content,
                ContentType = "text/html",
            };
        }

        public ActionResult Logout()
        {
            // Returning a SignOutResult will ask the cookies middleware to delete the local cookie created when
            // the user agent is redirected from the external identity provider after a successful authentication flow
            // and will redirect the user agent to the post_logout_redirect_uri specified by the client application.
            return SignOut("Cookies", "oidc");
        }

        //public async Task Logout()
        //{
        //    await HttpContext.SignOutAsync("Cookies");
        //    await HttpContext.SignOutAsync("oidc", new AuthenticationProperties()
        //    {
        //        RedirectUri = "/signedout"
        //    });
        //    //return;
        //    ////return new SignOutResult(new[] { "oidc", "Cookies" });
        //}

    }
}
