using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using IdentityModel;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using System.Linq;

namespace Microsoft.AspNetCore.Authentication.EDevlet
{
    internal class EDevletHandler : OAuthHandler<EDevletOptions>
    {

        public EDevletHandler(IOptionsMonitor<EDevletOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        { }

        protected async override Task<OAuthTokenResponse> ExchangeCodeAsync(string code, string redirectUri)
        {
            var tokenRequestParameters = new Dictionary<string, string>()
            {
                { "client_id", Options.ClientId },
                { "redirect_uri", redirectUri },
                { "client_secret", Options.ClientSecret },
                { "code", code },
                { "grant_type", "authorization_code" }
            };
            var requestContent = new FormUrlEncodedContent(tokenRequestParameters);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, Options.TokenEndpoint);
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Content = requestContent;
            var response = await Backchannel.SendAsync(requestMessage, Context.RequestAborted);
            if (response.IsSuccessStatusCode)
            {
                var payload = JObject.Parse(await response.Content.ReadAsStringAsync());
                Logger.LogInformation("EDevlet-Token has been received: " + payload);
                return OAuthTokenResponse.Success(payload);
            }
            else
            {
                var error = "OAuth token endpoint failure: ";
                Logger.LogCritical("An error occured when receiving e-devlet token: " + await response.Content.ReadAsStringAsync());
                return OAuthTokenResponse.Failed(new Exception(error));
            }
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var kimlikJson = JObject.Parse(await GetJson("Kimlik-Dogrula", tokens.AccessToken));
            var userJson = JObject.Parse(await GetJson("Ad-Soyad", tokens.AccessToken))["kullaniciBilgileri"]; // Temel-Bilgileri Adres-Bilgileri Iletisim-Bilgileri
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtClaimTypes.Subject, userJson["kimlikNo"].ToString()));
            claims.Add(new Claim(JwtClaimTypes.Name, userJson["ad"].ToString() + " " + userJson["soyad"].ToString()));
            claims.Add(new Claim(JwtClaimTypes.GivenName, userJson["ad"].ToString()));
            claims.Add(new Claim(JwtClaimTypes.FamilyName, userJson["soyad"].ToString()));
            claims.Add(new Claim(EDevletDefaults.LoginMethodClaimName, kimlikJson["level"].ToString()));
            claims.Add(new Claim(EDevletDefaults.AccessTokenPropName, tokens.AccessToken));
            properties.Items[EDevletDefaults.AccessTokenPropName] = tokens.AccessToken;
            var scope = properties.Items["scope"].Split(" ");
            if (scope.Contains("address"))
            {
                var adresJson = JObject.Parse(await GetJson("Adres-Bilgileri", tokens.AccessToken))["adresBilgileri"];
                claims.Add(new Claim(JwtClaimTypes.Address, adresJson.ToString()));
            }
            if (scope.Contains("personal_info"))
            {
                var temelJson = JObject.Parse(await GetJson("Temel-Bilgileri", tokens.AccessToken))["kullaniciBilgileri"];
                claims.Add(new Claim("marital_status", temelJson["medeniHal"].ToString()));
                claims.Add(new Claim("mother_name", temelJson["anneAd"].ToString()));
                claims.Add(new Claim("father_name", temelJson["babaAd"].ToString()));
                claims.Add(new Claim(JwtClaimTypes.BirthDate, temelJson["dogumTarihi"].ToString()));
                claims.Add(new Claim(JwtClaimTypes.Gender, temelJson["cinsiyet"].ToString()));
            }
            if (scope.Contains("communication_info"))
            {
                var iletisimJson = JObject.Parse(await GetJson("Iletisim-Bilgileri", tokens.AccessToken))["kullaniciBilgileri"];
                claims.Add(new Claim(JwtClaimTypes.PhoneNumber, iletisimJson["cepTelefon"].ToString()));
                claims.Add(new Claim(JwtClaimTypes.Email, iletisimJson["eposta"].ToString()));
            }
            identity.AddClaims(claims);
            var principal = new ClaimsPrincipal(identity);

            return new AuthenticationTicket(new ClaimsPrincipal(identity), properties, Options.SignInScheme);
        }

        protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
        {
            var scope = properties.Items["scope"];
            scope =  scope.Replace("openid", "Kimlik-Dogrula");
            scope = scope.Replace("profile", "Ad-Soyad");
            scope = scope.Replace("personal_info", "Temel-Bilgileri");
            scope = scope.Replace("address", "Adres-Bilgileri");
            scope = scope.Replace("communication_info", "Iletisim-Bilgileri");
            var state = Options.StateDataFormat.Protect(properties);
            var parameters = new Dictionary<string, string>
            {
                { "client_id", Options.ClientId },
                { "scope", scope},
                { "response_type", "code" },
                { "redirect_uri", redirectUri },
                { "state", state },
            };
            return QueryHelpers.AddQueryString(Options.AuthorizationEndpoint, parameters);
        }

        private async Task<string> GetJson(string scope, string access_token)
        {
            var endpoint = QueryHelpers.AddQueryString(Options.UserInformationEndpoint, "access_token", access_token);
            var data = "clientId=" + Options.ClientId;
            data += "&accessToken=" + access_token;
            data += "&resourceId=1&kapsam=" + scope;
            var contentPost = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");
            var userRequestResponse = await Backchannel.PostAsync(new Uri(Options.UserInformationEndpoint), contentPost).ContinueWith(
                  (postTask) => postTask.Result.EnsureSuccessStatusCode());
            return await userRequestResponse.Content.ReadAsStringAsync();
        }
    }
}
























////protected override async Task<AuthenticationTicket> CreateTicketAsync(
////    ClaimsIdentity identity,
////    AuthenticationProperties properties,
////    OAuthTokenResponse tokens)
////{
////    // Get the Google user
////    var request = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);
////    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);

////    var response = await Backchannel.SendAsync(request, Context.RequestAborted);
////    if (!response.IsSuccessStatusCode)
////    {
////        throw new HttpRequestException($"An error occurred when retrieving user information ({response.StatusCode}). Please check if the authentication information is correct and the corresponding Google+ API is enabled.");
////    }

////    var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

////    var principal = new ClaimsPrincipal(identity);
////    var ticket = new AuthenticationTicket(principal, properties, Options.AuthenticationScheme);
////    var context = new OAuthCreatingTicketContext(ticket, Context, Options, Backchannel, tokens, payload);

////    return context.Ticket;
////}
////var data = "client_secret=" + Options.ClientSecret;
////data += "&grant_type=authorization_code";
////data += "&redirect_uri=" + redirectUri;
////data += "&code=" + code;
////data += "&client_id=" + Options.ClientId;
////HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Options.TokenEndpoint);
////byte[] byteArray = Encoding.UTF8.GetBytes(data);
////// Set the ContentType property of the WebRequest.
////request.ContentType = "application/x-www-form-urlencoded";
////request.Method = "POST";
////// Set the ContentLength property of the WebRequest.
////request.ContentLength = byteArray.Length;

////// Get the request stream.
////Stream dataStream = request.GetRequestStream();
////// Write the data to the request stream.
////dataStream.Write(byteArray, 0, byteArray.Length);
////// Close the Stream object.
////dataStream.Close();
////using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
////{
////    if (response.StatusCode != HttpStatusCode.OK)
////        throw new Exception(String.Format(
////        "Server error (HTTP {0}: {1}).",
////        response.StatusCode,
////        response.StatusDescription));
////    var responsestream = response.GetResponseStream();
////    string json = new StreamReader(responsestream).ReadToEnd();
////    var payload = JObject.Parse(json);
////    throw new Exception("code =" + code + payload.ToString());
////    return OAuthTokenResponse.Success(payload);
////}