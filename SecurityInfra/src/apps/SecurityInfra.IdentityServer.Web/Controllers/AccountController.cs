using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SecurityInfra.IdentityServer.Web.Infrastructure.Ldap;
using SecurityInfra.IdentityServer.Web.Infrastructure.Validators;
using SecurityInfra.IdentityServer.Web.ViewModels;

namespace SecurityInfra.IdentityServer.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly ILdapAuthenticationService _ldapAuthenticationService;
        private readonly IConfiguration _configuration;
        private readonly IClientStore _clientStore;
        private readonly UserActivity.IUserActivityRepository _userActivityRepository;
        private readonly IIpAddressValidator _ipAddressValidator;

        public AccountController(
            ILoggerFactory loggerFactory,
            IIdentityServerInteractionService interaction,
            ILdapAuthenticationService ldapAuthenticationService,
            IConfiguration configuration,
            IClientStore clientStore,
            UserActivity.IUserActivityRepository userActivityRepository,
            IIpAddressValidator ipAddressValidator)
        {
            _logger = loggerFactory.CreateLogger<AccountController>();
            _interaction = interaction;
            _ldapAuthenticationService = ldapAuthenticationService;
            _configuration = configuration;
            _clientStore = clientStore;
            _userActivityRepository = userActivityRepository;
            _ipAddressValidator = ipAddressValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = "/")
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

            if (context != null)
            {
                var client = await _clientStore.FindClientByIdAsync(context.ClientId);

                if (!client.EnableLocalLogin)
                {
                    return await ExternalLogin("EDevlet", returnUrl);
                }
            }
            if (!_ipAddressValidator.IsIpAddressInternal())
            {
                return await ExternalLogin("EDevlet", returnUrl);
            }
            return View(new LoginModel()
            {
                ReturnUrl = System.Net.WebUtility.UrlEncode(returnUrl),
                IsLocalContext = context == null
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string button)
        {
            if (!_ipAddressValidator.IsIpAddressInternal())
            {
                return Forbid();
            }
            model.IsLocalContext = _interaction.GetAuthorizationContextAsync(model.ReturnUrl).Result == null;
            if (ModelState.IsValid)
            {
                var loginResult = _ldapAuthenticationService.AuthenticateUser(model.Username, model.Password);
                if (loginResult.IsSucceed)
                {
                    await RaiseUdhbSignInEvent(loginResult.Principal, "LOCAL");
                    await HttpContext.SignInAsync(
                       IdentityServerConstants.DefaultCookieAuthenticationScheme, loginResult.Principal);
                    return Redirect(model.ReturnUrl);
                }
                else if (loginResult.ErrorCode == "ENTRYNOTFOUND")
                {
                    ModelState.AddModelError("", "Kullanıcı adı ve şifre uyuşmuyor");
                    return View(model);
                }
                else if (loginResult.ErrorCode == "SUBNOTFOUND")
                {
                    ModelState.AddModelError("", "Bu hesapla ilişkili bir kimlik numarası yok, Bilgi İşlem ile iletişime geçiniz");
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError("", "Beklenmeyen bir durum oluştu.");
                    return View(model);
                }
            }
            ModelState.ClearValidationState("Username");
            ModelState.ClearValidationState("Password");
            ModelState.AddModelError("", "Kullanıcı adı ve şifre uyuşmuyor");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLogin(string provider, string returnUrl)
        {
            returnUrl = Url.Action("ExternalLoginCallback", new { returnUrl });

            var props = new AuthenticationProperties
            {
                RedirectUri = returnUrl
            };
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            var scope = "";
            if (context == null)
            {
                scope = "openid profile personal_info address communication_info";
            }
            else
            {
                var client = await _clientStore.FindClientByIdAsync(context.ClientId);
                var s = client.Properties.FirstOrDefault(x => x.Key == "scope");
                scope = s.Value;
            }
            props.Items.Add("scope", scope);
            return new ChallengeResult(provider, props);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null)
        {
            var result = await HttpContext.AuthenticateAsync(
                IdentityServerConstants.ExternalCookieAuthenticationScheme);
            if (result?.Succeeded != true)
            {
                throw new Exception("External authentication error");
            }
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, result.Principal.FindFirstValue(JwtClaimTypes.Name)));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, result.Principal.FindFirstValue(JwtClaimTypes.Subject)));

            var p = new ClaimsPrincipal(new ClaimsIdentity(result?.Principal?.Identity, claims));
            await HttpContext.SignOutAsync(
               IdentityServerConstants.ExternalCookieAuthenticationScheme);
            await HttpContext.SignInAsync(
                      IdentityServerConstants.DefaultCookieAuthenticationScheme, p);
            await RaiseUdhbSignInEvent(p, "EXTERNAL");
            return Redirect(returnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            return await LogoutPost(logoutId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutPost(string logoutId)
        {
            await HttpContext.SignOutAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme);
            await _interaction.RevokeTokensForCurrentSessionAsync();
            var r = await _interaction.GetLogoutContextAsync(logoutId);
            ViewBag.PostLogoutRedirectUri = r?.PostLogoutRedirectUri;
            ViewBag.SignOutIFrameUrl = r?.SignOutIFrameUrl;
            await RaiseUdhbSignOutEvent(User);
            return View("LoggedOut");
        }

        private async Task RaiseUdhbSignOutEvent(ClaimsPrincipal p)
        {
            var activityLog = new UserActivity.UserActivity();
            activityLog.ActionName = "LOGOUT";
            activityLog.ActionTitle = "Oturum Kapatma";
            activityLog.CreatedAt = DateTime.Now;
            activityLog.UserName = p.Identity.Name;
            activityLog.UserId = p.GetUserId();
            activityLog.RequestId = HttpContext.TraceIdentifier;
            activityLog.UserIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            activityLog.TenantId = "SSO";
            activityLog.Message = $"Oturum kapatıldı";
            await _userActivityRepository.Add(activityLog);
        }

        private async Task RaiseUdhbSignInEvent(ClaimsPrincipal p, string loginType)
        {
            var activityLog = new UserActivity.UserActivity();
            activityLog.ActionName = "LOGIN";
            activityLog.ActionTitle = "Oturum Açma";
            activityLog.CreatedAt = DateTime.Now;
            activityLog.UserName = p.Identity.Name;
            activityLog.Message = $"{Request.HttpContext.Connection.RemoteIpAddress} ip adresi üzerinde {loginType} ile oturum açıldı";
            activityLog.RequestId = HttpContext.TraceIdentifier;
            activityLog.UserIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            activityLog.UserId = p.GetUserId();
            activityLog.TenantId = "SSO";
            await _userActivityRepository.Add(activityLog);
        }
    }
}
