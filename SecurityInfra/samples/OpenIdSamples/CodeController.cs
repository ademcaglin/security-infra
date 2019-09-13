using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using IdentityModel.Client;
using IdentityModel;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Globalization;
using System.Security.Claims;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UdhbGiris.Samples
{
    public class CodeController : Controller
    {
        [Authorize]
        public IActionResult Secure()
        {
            var access_token = HttpContext.GetTokenAsync("access_token");
            //var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token.Result);
            //using (var response = httpClient.GetAsync("http://localhost:49908/connect/userinfo").Result)
            //{
            //    string responseData = response.Content.ReadAsStringAsync().Result;
            //}
            return Content(User.FindFirst("given_name").Value);
        }

        public IActionResult Manual()
        {
        
            var parameters = new Dictionary<string, string>
            {
                { "client_id",  "codesample"},
                { "scope", "openid profile" },
                { "response_type", "code" },
                { "redirect_uri", "http://localhost:5005/signin-oidc" },
                { "state", "random_state" },
                { "response_mode", "form_post"},
                { "nonce", "random_nonce"}
            };
            var authorizeUrl = QueryHelpers.AddQueryString("https://giris.udhb.gov.tr/connect/authorize", parameters);
            return Redirect(authorizeUrl);
        }
        //public async Task<IActionResult> RenewTokens()
        //{
        //    var disco = await DiscoveryClient.GetAsync("");
        //    if (disco.IsError) throw new Exception(disco.Error);

        //    var tokenClient = new TokenClient(disco.TokenEndpoint, "mvc.hybrid", "secret");
        //    var rt = await HttpContext.GetTokenAsync("refresh_token");
        //    var tokenResult = await tokenClient.RequestRefreshTokenAsync(rt);

        //    if (!tokenResult.IsError)
        //    {
        //        var old_id_token = await HttpContext.GetTokenAsync("id_token");
        //        var new_access_token = tokenResult.AccessToken;
        //        var new_refresh_token = tokenResult.RefreshToken;

        //        var tokens = new List<AuthenticationToken>();
        //        tokens.Add(new AuthenticationToken { Name = OpenIdConnectParameterNames.IdToken, Value = old_id_token });
        //        tokens.Add(new AuthenticationToken { Name = OpenIdConnectParameterNames.AccessToken, Value = new_access_token });
        //        tokens.Add(new AuthenticationToken { Name = OpenIdConnectParameterNames.RefreshToken, Value = new_refresh_token });

        //        var expiresAt = DateTime.UtcNow + TimeSpan.FromSeconds(tokenResult.ExpiresIn);
        //        tokens.Add(new AuthenticationToken { Name = "expires_at", Value = expiresAt.ToString("o", CultureInfo.InvariantCulture) });

        //        var info = await HttpContext.AuthenticateAsync("Cookies");
        //        info.Properties.StoreTokens(tokens);
        //        await HttpContext.SignInAsync("Cookies", info.Principal, info.Properties);

        //        return Redirect("~/Home/Secure");
        //    }

        //    ViewData["Error"] = tokenResult.Error;
        //    return Content("Error");
        //}

        public IActionResult Logout()
        {
            return new SignOutResult(new[] { "Cookies", "oidc" });
        }

    }
}
