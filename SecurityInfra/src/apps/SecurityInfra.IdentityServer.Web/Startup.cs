using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SecurityInfra.Configuration.Mongo;
using SecurityInfra.IdentityServer.Web.Infrastructure;
using SecurityInfra.UserActivity;
using IdentityServer4.Validation;

namespace SecurityInfra.IdentityServer.Web
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();
            var idsrv = services.AddIdentityServer()
                .AddProfileService<ProfileService>();
            idsrv.AddResourceStore<ResourceStore>()
                 .AddClientStore<ClientStore>();
            idsrv.AddCustomTokenRequestValidator<Infrastructure.Validators.CustomTokenRequestValidator>();
            var cerFileName = Configuration["IDPCERTIFICATE_FILENAME"];
            var cerFilePwd = Configuration["IDPCERTIFICATE_FILEPWD"];
            var cer = new X509Certificate2(cerFileName, cerFilePwd);
            idsrv.AddSigningCredential(cer);
            if (HostingEnvironment.IsDevelopment())
            {
                services.AddSingleton<Infrastructure.Ldap.ILdapAuthenticationService,
                    Infrastructure.Ldap.TestLdapAuthenticationService>();
            }
            else
            {
                services.AddSingleton<Infrastructure.Ldap.ILdapAuthenticationService,
                    Infrastructure.Ldap.LdapAuthenticationService>();
            }
            services.AddAuthentication()
                .AddEDevlet(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = Configuration["EDEVLET_CLIENTID"];
                    options.ClientSecret = Configuration["EDEVLET_CLIENTSECRET"];
                    options.AuthorizationEndpoint = Configuration["EDEVLET_AUTHORIZEENDPOINT"];
                    options.TokenEndpoint = Configuration["EDEVLET_TOKENENDPOINT"];
                    options.UserInformationEndpoint = Configuration["EDEVLET_USERINFOENDPOINT"];
                    options.Events = new OAuthEvents()
                    {
                        OnRemoteFailure = ctx =>
                        {
                            if (!HostingEnvironment.IsDevelopment())
                            {
                                ctx.Response.Redirect("/error");
                                ctx.HandleResponse();
                            }
                            return Task.FromResult(0);
                        }
                    };
                });
            services.AddIdsrvConfiguration(options =>
            {
                options.ConnectionString = Configuration["CONFIGURATIONDB_CONSTR"];
                options.DatabaseName = "ConfigurationDb";
                options.IsSSL = false;
            });
            services.AddScoped<IUserActivityRepository, Infrastructure.UserActivity.UserActivityRepository>();
            services.AddScoped<Infrastructure.Validators.IIpAddressValidator, Infrastructure.Validators.IpAddressValidator>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}



/*services.AddAutoMapper();
var redisConfig = $"{Configuration["REDIS_HOST"]}:{Configuration["REDIS_PORT"]}";
services.AddDataProtection()
  .PersistKeysToStackExchangeRedis(StackExchange.Redis.ConnectionMultiplexer.Connect(redisConfig),
                      "DataProtection-Keys");

var idsrv = services.AddIdentityServer()
    .AddProfileService<ProfileService>();
idsrv.AddOperationalStore(options =>
{
    options.RedisConnectionString = redisConfig;
    options.KeyPrefix = "SecurityInfra.Operational";
})
.AddRedisCaching(options =>
{
    options.RedisConnectionString = redisConfig;
    options.KeyPrefix = "SecurityInfra.Caching";
})
.AddResourceStoreCache<ResourceStore>()
.AddClientStoreCache<ClientStore>();
var cerFileName = Configuration["IDPCERTIFICATE_FILENAME"];
var cerFilePwd = Configuration["IDPCERTIFICATE_FILEPWD"];
var cer = new X509Certificate2(cerFileName, cerFilePwd);
idsrv.AddSigningCredential(cer);
if (HostingEnvironment.IsDevelopment())
{
    services.AddSingleton<Infrastructure.Ldap.ILdapAuthenticationService,
        Infrastructure.Ldap.TestLdapAuthenticationService>();
}
else
{
    services.AddSingleton<Infrastructure.Ldap.ILdapAuthenticationService,
        Infrastructure.Ldap.LdapAuthenticationService>();
}
services.AddAuthentication()
    .AddEDevlet(options =>
    {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
        options.ClientId = Configuration["EDEVLET_CLIENTID"];
        options.ClientSecret = Configuration["EDEVLET_CLIENTSECRET"];
        options.AuthorizationEndpoint = Configuration["EDEVLET_AUTHORIZEENDPOINT"];
        options.TokenEndpoint = Configuration["EDEVLET_TOKENENDPOINT"];
        options.UserInformationEndpoint = Configuration["EDEVLET_USERINFOENDPOINT"];
        options.Events = new OAuthEvents()
        {
            OnRemoteFailure = ctx =>
            {
                if (!HostingEnvironment.IsDevelopment())
                {
                    ctx.Response.Redirect("/error");
                    ctx.HandleResponse();
                }
                return Task.FromResult(0);
            }
        };
    });
services.AddIdsrvConfiguration(options =>
{
    options.ConnectionString = Configuration["CONFIGURATIONDB_CONSTR"];
    options.DatabaseName = "ConfigurationDb";
    options.IsSSL = false;
});
services.AddUserActivity(options =>
{
    options.ElasticConnectionSettings = Configuration["USERACTIVITY_CONSTR"];
});*/

//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.AddLib();
//            services.AddAutoMapper();
//            services.AddCache();

//            services.AddScoped<ILdapAuthenticationService, LdapAuthenticationService>();

//            services.AddScoped<ILdapAuthenticationService, LdapAuthenticationService>();

//            var idsrv = services.AddIdentityServer()
//                .AddProfileService<UdhbProfileService>();

//            var redisConfig = $"{Configuration["UDHBGIRISREDIS_HOST"]}:{Configuration["UDHBGIRISREDIS_PORT"]}";
//            services.AddDataProtection()
//              .PersistKeysToRedis(StackExchange.Redis.ConnectionMultiplexer.Connect(redisConfig),
//                                  "DataProtection-Keys");
//            idsrv.AddResourceStore<UdhbResourceStore>()
//                 .AddClientStore<UdhbClientStore>();
//            //idsrv.AddResourceStoreCache<UdhbResourceStore>()
//            //     .AddClientStoreCache<UdhbClientStore>();
//            idsrv.AddOperationalStore(options =>
//            {
//                options.RedisConnectionString = redisConfig;
//                options.KeyPrefix = "UdhbGiris.Operational";
//            })
//            .AddRedisCaching(options =>
//            {
//                options.RedisConnectionString = redisConfig;
//                options.KeyPrefix = "UdhbGiris.Caching";
//            });
//            var cerFileName = Configuration["UDHBGIRISCERTIFICATE_FILENAME"];
//            var cerFilePwd = Configuration["UDHBGIRISCERTIFICATE_FILEPWD"];
//            var cer = new X509Certificate2(cerFileName, cerFilePwd);
//            idsrv.AddSigningCredential(cer);
//            services.AddAuthentication()
//                .AddCookie("LdapCookies", options =>
//                {
//                    options.ExpireTimeSpan = new TimeSpan(0, 0, 300);
//                })
//                .AddEDevlet(options =>
//                {
//                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
//                    options.ClientId = Configuration["EDEVLET_CLIENTID"];
//                    options.ClientSecret = Configuration["EDEVLET_CLIENTSECRET"];
//                    options.AuthorizationEndpoint = Configuration["EDEVLET_AUTHORIZEENDPOINT"];
//                    options.TokenEndpoint = Configuration["EDEVLET_TOKENENDPOINT"];
//                    options.UserInformationEndpoint = Configuration["EDEVLET_USERINFOENDPOINT"];
//                    options.Events = new OAuthEvents()
//                    {
//                        OnRemoteFailure = ctx =>
//                        {
//                            if (!HostingEnvironment.IsDevelopment())
//                            {
//                                ctx.Response.Redirect("/error/edevlet");
//                                ctx.HandleResponse();
//                            }
//                            return Task.FromResult(0);
//                        }
//                    };
//                });
//            services.Configure<UdhbGirisConfigurationDbOptions>(opt =>
//            {
//                opt.IsSSL = false;
//                opt.DatabaseName = "UdhbGirisConfigurationDb";
//                opt.ConnectionString = Configuration["UDHBGIRISCONFIGURATIONDB_CONSTR"];
//            });
//            services.AddScoped<UdhbGirisConfigurationDbContext>();
//            services.AddDbContext<ActivityDbContext>(options =>
//                 options.UseSqlServer(Configuration["USERACTIVITYYDB_CONSTR"]));
//            services.AddGovIdentity(opt =>
//            {
//                opt.ConnectionString = Configuration["UDHBGIRISIDENTITYDB_CONSTR"];
//            });
//            services.AddMvc();
//        }

//        public void Configure(IApplicationBuilder app)
//        {
//            if (HostingEnvironment.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            app.UseUdhbDynamicScheme();
//            app.UseUdhbExceptionHandler();
//            app.UseIdentityServer();
//            app.UseStaticFiles();
//            app.UseMvcWithDefaultRoute();
//        }