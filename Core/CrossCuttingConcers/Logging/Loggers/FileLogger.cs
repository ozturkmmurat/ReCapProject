using Core.CrossCuttingConcers.Logging.Serilog;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Core.CrossCuttingConcers.Logging.Serilog.ConfigurationModels;
using Core.Utilities.Messages;
using Serilog;

namespace Core.CrossCuttingConcers.Logging.Loggers
{
    public class FileLogger : LoggerServiceBase
    {
        public FileLogger()
        {
            var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

            var logConfig = configuration.GetSection("SeriLogConfigurations:FileLogConfiguration")
                                .Get<FileLogConfiguration>() ??
                            throw new Exception(SerilogMessages.NullOptionsMessage);
            var logFilePath = string.Format("{0}{1}", "C://" + logConfig.FolderPath, ".json");

            Logger = new LoggerConfiguration()
                .WriteTo.File(
                    logFilePath,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: null,
                    fileSizeLimitBytes: 5000000,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}",
                    shared: true)
                .CreateLogger();
        }
    }
}
