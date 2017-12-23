using System;
using System.Threading;
using Flogging.Core;
using Flogging.Core.Performance;

namespace FloggingConsole
{
    class Program
    {
        static void Main(string[] args) {
            var fd = GetFullLogDetail("Starting the application");
            Flogger.WriteDiagnosticLog(fd);

            var tracker = new PerformanceTracker(
                name: $"{nameof(FloggingConsole)}_Execution", 
                userId: "", 
                userName: fd.UserName, 
                location: fd.Location, 
                product: fd.Product, 
                layer: fd.Layer
            );

            Thread.Sleep(25);

            try {
                var fakeException = new Exception("Soemthing bad has happened!");
                fakeException.Data.Add("input parameter", "nothing to see here");
                throw fakeException;
            }
            catch (Exception ex) {
                fd = GetFullLogDetail("", ex);
                Flogger.WriteErrorLog(fd);
            }

            Thread.Sleep(25);

            fd = GetFullLogDetail("Used flogging console");
            Flogger.WriteUsageLog(fd);

            fd = GetFullLogDetail("Stopping application");
            Flogger.WriteDiagnosticLog(fd);

            tracker.Stop();
        }

        private static FullLogDetail GetFullLogDetail(string message, Exception ex = null)
            => new FullLogDetail {
                Product = "Flogger",
                Location = nameof(FloggingConsole),
                Layer = "Job",
                UserName = Environment.UserName,
                Hostname = Environment.MachineName,
                Message = message,
                Exception = ex
            };
    }
}
