using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdhbGiris.Samples
{
    public class ManualStartup
    {
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddMvc();

                services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.Cookie.Name = "mvcmanual";
                    });
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
