using FluentAssertions;
using Instrumentation.Logging.Volatile;
using NUnit.Framework;

namespace Instrumentation.Logging.UnitTests
{
    [TestFixture]
    public class CallCountLogTests
    {
        [Test]
        public void Increment()
        {
            var log = new CallCountLog("key");

            log.Log();
            log.Log();
            log.CallCount.Should().Be(2);
        }
    }
}
