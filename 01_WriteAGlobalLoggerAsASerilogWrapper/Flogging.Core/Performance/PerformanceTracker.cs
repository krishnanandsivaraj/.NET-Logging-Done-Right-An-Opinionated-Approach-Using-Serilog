using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Flogging.Core.Performance
{
    public class PerformanceTracker
    {
        public PerformanceTracker(
            string name, string userId, string userName, string location, 
            string product, string layer)
        {
            _stopwatch = new Stopwatch();

            _fullLogDetail = new FullLogDetail {
                Message = name,
                UserId = userId,
                UserName = userName,
                Product = product,
                Layer = layer,
                Location = location,
                Hostname = Environment.MachineName                
            };
            _fullLogDetail.AdditionalInfo.Add(
                key: "Started", 
                value: DateTime.Now.ToString(CultureInfo.InvariantCulture)
            );
        }

        public PerformanceTracker(
            string name, string userId, string userName, string location,
            string product, string layer, Dictionary<string, object> performanceParameters)
            : this(name, userId, userName, location, product, layer
        ) => performanceParameters?.ToList().ForEach(kvp => 
            _fullLogDetail.AdditionalInfo.Add(
                key: $"input-{kvp.Key}", 
                value: kvp.Value
            )
          );

        private readonly Stopwatch _stopwatch;
        private readonly FullLogDetail _fullLogDetail;

        public void Stop() {
            _stopwatch.Stop();
            _fullLogDetail.ElpasedMilliseconds = _stopwatch.ElapsedMilliseconds;
            Flogger.WritePerformanceLog(_fullLogDetail);
        }
    }
}
