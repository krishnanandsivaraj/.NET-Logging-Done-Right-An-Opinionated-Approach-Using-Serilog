using System;
using System.Configuration;
using System.IO;
using Serilog;
using Serilog.Events;

namespace Flogging.Core
{
    public static class Flogger
    {
        static Flogger() {
            _performanceLogger = CreateNewLogger("performance");
            _usageLogger = CreateNewLogger("usage");
            _errorLogger = CreateNewLogger("error");
            _diagnosticLogger = CreateNewLogger("diagnostic");
        }        

        private static readonly ILogger _performanceLogger;
        private static readonly ILogger _usageLogger;
        private static readonly ILogger _errorLogger;
        private static readonly ILogger _diagnosticLogger;

        private static ILogger CreateNewLogger(string loggerName)
            => new LoggerConfiguration()
                .WriteTo.File(
                    path: CalculateLoggerPath(loggerName)
                )
                .CreateLogger();

        private static string CalculateLoggerPath(string loggerName)
            => Path.Combine(Directory.GetCurrentDirectory(), "logs", $"{loggerName}.txt");


        public static void WritePerformanceLog(FullLogDetail fullLogDetail)
            => Write(fullLogDetail, _performanceLogger);
        public static void WriteUsageLog(FullLogDetail fullLogDetail)
            => Write(fullLogDetail, _usageLogger);
        public static void WriteErrorLog(FullLogDetail fullLogDetail)
            => Write(fullLogDetail, _errorLogger);
        public static void WriteDiagnosticLog(FullLogDetail fullLogDetail) {
            if (ShouldWriteDiagnostics())
                Write(fullLogDetail, _diagnosticLogger);
        }

        private static bool ShouldWriteDiagnostics()
            => Convert.ToBoolean(ConfigurationManager.AppSettings["EnableDiagnostics"]);

        private static void Write<TFullLogDetail>(
            TFullLogDetail fullLogDetail, ILogger logger)
            where TFullLogDetail : FullLogDetail
            => logger?.Write(LogEventLevel.Information, "{@FullLogDetail}", fullLogDetail);        
    }
}
