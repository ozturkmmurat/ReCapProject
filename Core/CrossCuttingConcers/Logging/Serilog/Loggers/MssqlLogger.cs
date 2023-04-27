using Core.CrossCuttingConcers.Logging.Serilog;
using Core.CrossCuttingConcers.Logging.Serilog.ConfigurationModels;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers

{
    public class MssqlLogger : LoggerServiceBase
    {
        public MssqlLogger()
        {
            var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

            var logConfig = configuration.GetSection("SeriLogConfigurations:MsSqlConfiguration")
                                .Get<MsSqlConfiguration>() ??
                            throw new Exception(SerilogMessages.NullOptionsMessage);
            var sinkOpts = new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true };

            var columnOpts = new ColumnOptions();
            columnOpts.Store.Remove(StandardColumn.Message);
            columnOpts.Store.Remove(StandardColumn.Properties);

            var seriLogConfig = new LoggerConfiguration()
                .WriteTo.MSSqlServer(connectionString: logConfig.ConnectionString, sinkOptions: sinkOpts, columnOptions: columnOpts)
                .CreateLogger();

            Logger = seriLogConfig;
        }
    }
}
