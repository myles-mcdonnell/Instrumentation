using Instrumentation.Logging.Contract;
using Moq;
using NUnit.Framework;

namespace Instrumentation.Aspects.UnitTests
{
    [TestFixture]
    public class MethodExecutionTimeTests
    {
        [Test]
        public void Test()
        {
            var methodExecutionTimeLog = new Mock<IMethodExecutionTimeLog>();

            LogProvider.MethodExecutionTimeLogFactory = instanceName => methodExecutionTimeLog.Object;

            new Target().Method();
            new Target().Method();

            methodExecutionTimeLog.Verify(l => l.Log(It.IsAny<long>()), Times.Exactly(2));
        }

        private class Target
        {
            [MethodExecutionTime]
            public void Method() { }
        }
    }
}
