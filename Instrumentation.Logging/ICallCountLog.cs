namespace Instrumentation.Logging.Contract
{
    public interface ICallCountLog
    {
        void Log();
        long CallCount { get; }
    }
}
