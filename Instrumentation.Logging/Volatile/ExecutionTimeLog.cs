using System.Threading;
using Instrumentation.Logging.Contract;

namespace Instrumentation.Logging.Volatile
{
    public class ExecutionTimeLog : CallCountLog, IMethodExecutionTimeLog
    {
        private readonly object _calcLock = new object();
        private long _totalExecutionTimeMs;

        public ExecutionTimeLog(string key) : base(key)
        {}

        public void Log(long executionTimeMs)
        {
            Log();
            Interlocked.Add(ref _totalExecutionTimeMs, executionTimeMs);
        }

        public decimal AverageExecutionTime()
        {
            lock (_calcLock)
            {
                if (_totalExecutionTimeMs == 0 || CallCount == 0)
                    return 0;

                return (decimal)_totalExecutionTimeMs / CallCount;
            }
        }
    }
}
