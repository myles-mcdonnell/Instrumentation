using FluentAssertions;
using Instrumentation.Logging.Volatile;
using NUnit.Framework;

namespace Instrumentation.Logging.UnitTests
{
    [TestFixture]
    public class ExecutiontimeLogTests
    {
        [Test]
        public void Log()
        {
            var log = new ExecutionTimeLog("key");

            log.Log(10);
            log.Log(20);
            log.CallCount.Should().Be(2);
            log.AverageExecutionTime().Should().Be(15);
        }
    }
}
