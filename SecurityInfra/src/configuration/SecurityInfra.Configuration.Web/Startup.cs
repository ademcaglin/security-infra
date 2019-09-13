using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SecurityInfra.Configuration.Web.Infrastructure.AutofacModules;
using SecurityInfra.Configuration.Mongo;

namespace SecurityInfra.Configuration.Web
{
    public class Startup
    {
        protected IHostingEnvironment HostingEnvironment { get; }
        protected ILogger Logger { get; set; }
        protected IConfiguration Configuration { get; set; }
        public Startup(ILogger<Startup> logger,
             IHostingEnvironment hostingEnvironment,
             IConfiguration configuration)
        {
            HostingEnvironment = hostingEnvironment;
            Logger = logger;
            Configuration = configuration;
            Logger.LogInformation("App Started");
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddRepositories();
            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new MediatorModule());
            //container.RegisterModule(new ApplicationModule(Configuration["ConnectionString"]));

            return new AutofacServiceProvider(container.Build());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
