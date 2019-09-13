using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UdhbGiris.Samples
{
    public class CodeStartup
    {
        public CodeStartup()
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = "oidc";
            //})
            //    .AddCookie(options =>
            //    {
            //        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            //        options.Cookie.Name = "mvchybrid";
            //    })
            //    .AddOpenIdConnect("oidc", options =>
            //    {
            //        options.Authority = "https://giris.udhb.gov.tr";//"http://localhost:49908";//
            //        //options.RequireHttpsMetadata = false;

            //        options.ClientSecret = "codesample_pwd";//"lybs_genel_pwd";
            //        options.ClientId = "codesample";//"codeflow";
            //        options.ResponseType = "code";

            //        options.Scope.Clear();
            //        options.Scope.Add("openid");
            //        options.Scope.Add("profile");
                    
            //        //options.Scope.Add("lybs_genel");
            //        //options.Scope.Add("offline_access");

            //        //options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
            //        //options.ClaimActions.MapJsonKey(ClaimTypes.Name, "login");
            //        options.ClaimActions.MapJsonKey("birthday", "birthday");
            //        //options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email", ClaimValueTypes.Email);

            //        options.GetClaimsFromUserInfoEndpoint = true;
            //        options.SaveTokens = true;

            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            NameClaimType = JwtClaimTypes.Name,
            //            RoleClaimType = JwtClaimTypes.Role,
            //        };
            //        //options.Events.OnUserInformationReceived = context =>
            //        //{
            //        //    var u = context.Principal;
            //        //    return Task.CompletedTask;
            //        //};
            //    });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
