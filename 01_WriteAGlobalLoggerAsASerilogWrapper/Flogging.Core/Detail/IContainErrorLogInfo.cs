using System;

namespace Flogging.Core
{
    public interface IContainErrorLogInfo
    {
        Exception Exception { get; set; }
    }
}