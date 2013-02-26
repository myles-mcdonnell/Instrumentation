using System.Threading;
using Instrumentation.Logging.Contract;

namespace Intrumentation.Logging.Volatile
{
    public class CallCountLog : ICallCountLog
    {
        private readonly string _key;
        private long _callCount;

        public CallCountLog(string key)
        {
            _key = key;
        }

        public string Key
        {
            get { return _key; }
        }

        public long CallCount
        {
            get { return _callCount; }
        }

        public void Log()
        {
            Interlocked.Increment(ref _callCount);
        }
    }
}
