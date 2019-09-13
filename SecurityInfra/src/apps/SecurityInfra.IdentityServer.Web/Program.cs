using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using SecurityInfra.Common.Extensions;
using Serilog.Events;
using SecurityInfra.Configuration.Clients;
using IdentityServer4.Models;
using IdentityServer4;
using Microsoft.Extensions.DependencyInjection;
using SecurityInfra.Configuration.IdentityResources;
using SecurityInfra.Configuration.ApiResources;
using SecurityInfra.IdentityServer.Web.Infrastructure;

namespace SecurityInfra.IdentityServer.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            Seed.Create(host.Services);
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
             WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging((hostingContext, logging) =>
                {
                    var loggerConfiguration = new LoggerConfiguration()
                          .MinimumLevel.Verbose()
                          .Enrich.FromLogContext();
                    loggerConfiguration.AddInformationToFile();
                    loggerConfiguration.AddErrorToFile();
                    if (!string.IsNullOrEmpty(hostingContext.Configuration["GELFLOGGER_HOST"]))
                    {
                        loggerConfiguration.AddToGrayLog(hostingContext.Configuration["GELFLOGGER_HOST"],
                        hostingContext.Configuration["LOGGER_UYGULAMAADI"],
                        hostingContext.Configuration["LOGGER_MODULADI"],
                        hostingContext.HostingEnvironment.EnvironmentName);
                    }

                    AddCommandLogToFile(loggerConfiguration, hostingContext.Configuration["ACTIVITY_PATH"]);
                });

        public static LoggerConfiguration AddCommandLogToFile(LoggerConfiguration loggerConfiguration, string filePath)
        {
            var activityTemplate = "{Timestamp:HH:mm} [{Level}] {Message} {UserId} {UserName} {ActionName} {ActionTitle} {UserIpAddress} {Properties} {NewLine}";
            loggerConfiguration.WriteTo.Logger(fileLogger => fileLogger
                              .Filter.ByIncludingOnly(x =>
                                    x.Level == LogEventLevel.Information &&
                                    x.Properties.ContainsKey("ActionName"))
                              .WriteTo.RollingFile(outputTemplate: activityTemplate,
                                   pathFormat: filePath + "log-{Date}.txt"));
            return loggerConfiguration;
        }
    }
}
