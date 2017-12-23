using System;
using System.Collections.Generic;

namespace Flogging.Core
{
    public class FullLogDetail
        : IContainLocationLogInfo, IContainUserLogInfo, IContainErrorLogInfo,
        IContainPerformanceLogInfo, IContainAddionalLogInfo
    {
        public FullLogDetail() {
            Timestamp = DateTime.Now;
            AdditionalInfo = new Dictionary<string, object>();
        }

        #region General
        public DateTime Timestamp { get; }
        public string Message { get; set; }
        public string CorrelationId { get; set; }
        #endregion

        #region Location Detail
        public string Product { get; set; }
        public string Layer { get; set; }
        public string Location { get; set; }
        public string Hostname { get; set; }
        #endregion

        #region User Detail
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        #endregion

        #region Error Detail
        public Exception Exception { get; set; }
        #endregion

        #region Performance Detail
        public long? ElpasedMilliseconds { get; set; }
        #endregion

        #region Additional Info Detail
        public Dictionary<string, object> AdditionalInfo { get; set; }
        #endregion
    }
}
