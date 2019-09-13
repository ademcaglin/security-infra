using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.Graylog;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Common.Extensions
{
    public static class SerilogLoggerExtensions
    {
        public static LoggerConfiguration AddInformationToFile(this LoggerConfiguration loggerConfiguration, string infoBasePath = "bin\\Debug\\InfoLogs\\")
        {
            var logTemplate = "{Timestamp:HH:mm} [{Level}] {UserId} {Message}";
            loggerConfiguration.WriteTo.RollingFile(outputTemplate: logTemplate,
                pathFormat: infoBasePath + "log-{Date}.txt");
            return loggerConfiguration;
        }

        public static LoggerConfiguration AddErrorToFile(this LoggerConfiguration loggerConfiguration, string errorBasePath = "bin\\Debug\\ErrorLogs\\")
        {
            var logTemplate = "{Timestamp:HH:mm} [{Level}] {UserId} {Message} {NewLine} {Exception}";
            loggerConfiguration.WriteTo.Logger(fileLogger => fileLogger
                              .MinimumLevel.Error()
                              .WriteTo.RollingFile(outputTemplate: logTemplate, pathFormat: errorBasePath + "log-error-{Date}.txt")
                      );
            return loggerConfiguration;
        }
        public static LoggerConfiguration AddToGrayLog(this LoggerConfiguration loggerConfiguration,
            string gelfLogHost,
            string appName,
            string moduleName,
            string env)
        {
            loggerConfiguration.WriteTo.Graylog(new Serilog.Sinks.Graylog.Core.GraylogSinkOptions
            {
                HostnameOrAddress = gelfLogHost,
                Port = 12201
            });
            loggerConfiguration.Enrich.WithProperty("UygulamaAdi", appName);
            loggerConfiguration.Enrich.WithProperty("ModulAdi", moduleName);
            loggerConfiguration.Enrich.WithProperty("Environment", env);
            return loggerConfiguration;
        }

    }
}
