using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Logging
{
    public interface ILogger
    {
        void LogMessage(string message);

        void LogError(string error);

        void LogErrors(IEnumerable<string> errors);
    }
}
