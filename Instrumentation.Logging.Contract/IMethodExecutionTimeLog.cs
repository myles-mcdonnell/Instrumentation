namespace Instrumentation.Logging.Contract
{
    public interface IMethodExecutionTimeLog
    {
        void Log(long executionTimeMs);
        decimal AverageExecutionTime();
    }
}
