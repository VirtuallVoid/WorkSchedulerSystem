using Application.DTOs.Logs;
using Application.Interfaces.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class ExceptionHelper
    {
        public static async Task HandleExceptionAsync( LogRequestDto logObj, Exception ex, ILoggingRepository logger, bool isBusinessEvent = false)
        {
            try
            {
                if (isBusinessEvent)
                {
                    var errorLog = new LogRequestDto(
                        OperationName: logObj.OperationName,
                        Message: logObj.Message ?? "Unexpected business failure"
                    )
                    {
                        Exception = ex.ToString(),
                        Details = logObj.Details
                    };

                    await logger.InsertLogAsync(errorLog);
                }

                LogExceptionToFile(ex, logObj.OperationName, logObj.Details);
            }
            catch (Exception loggingEx)
            {
                Console.WriteLine($"Logging failed: {loggingEx.Message}");
            }
        }

        public static void LogExceptionToFile(Exception ex, string operationName, string details = null)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                    path: @"C:\Logs\Feelonym\Feelonym.log",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30,
                    shared: true,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();

            Log.Error(ex, "Exception during {OperationName}. Details: {Details}", operationName, details);
            Log.CloseAndFlush();
        }
    }
}
