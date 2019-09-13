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
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UdhbGiris.Samples
{
    public class CodeManualController : Controller
    {
        private static string IdToken;
        private const string Authority = "http://localhost:49908";
        /*[Route("codemanual")]
        public IActionResult Index()
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
        }*/

        [Route("manual")]
        public IActionResult Index()
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
            var authorizeUrl = QueryHelpers.AddQueryString(Authority + "/connect/authorize", parameters);
            return Redirect(authorizeUrl);
        }

        [Route("signout")]
        public IActionResult SignOut()
        {
            var parameters = new Dictionary<string, string>
            {
                { "id_token_hint",  IdToken},
                { "post_logout_redirect_uri", "http://localhost:5005/signout-callback-oidc" },
                { "state", "random_state" }
            };
            var authorizeUrl = QueryHelpers.AddQueryString(Authority + "/connect/endsession", parameters);
            return Redirect(authorizeUrl);
        }

        [Route("signout-callback-oidc")]
        public IActionResult SignedOut()
        {
            return Content("Logged Out");
        }

        [HttpPost]
        [Route("signin-oidc")]
        public IActionResult SignIn(string code, string state)
        {
            var httpClient = new HttpClient();
            var tokenEndpoint = Authority + "/connect/token";
            var tokenRequestParameters = new Dictionary<string, string>
            {
                { "client_id",  "codesample"},
                { "redirect_uri", "http://localhost:5005/signin-oidc" },
                { "code", code },
                { "grant_type", "authorization_code"},
                { "client_secret", "codesample_pwd"}
            };
            var requestContent = new FormUrlEncodedContent(tokenRequestParameters);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint);
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Content = requestContent;
            var response = httpClient.SendAsync(requestMessage).Result;
            if (response.IsSuccessStatusCode)
            {
                var payload = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                var access_token = payload.GetValue("access_token").ToString();
                var id_token = payload.GetValue("id_token").ToString();
                IdToken = id_token;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token.ToString());

                using (var responseSon = httpClient.GetAsync(Authority + "/connect/userinfo").Result)
                {
                    string responseData = responseSon.Content.ReadAsStringAsync().Result;
                    var sub = JObject.Parse(responseData)["sub"].ToString();
                    return Content(sub);
                }
            }
            return null;
        }
        
    }
}
