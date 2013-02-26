using Instrumentation.Logging.Contract;
using Moq;
using NUnit.Framework;

namespace Instrumentation.Aspects.UnitTests
{
    [TestFixture]
    public class MethodCallCountTests
    {
        [Test]
        public void Test()
        {
            var callCountLog = new Mock<ICallCountLog>();

            LogProvider.CallCountLogFactory = instanceName =>  callCountLog.Object;

            var target = new Target();

            target.Method();
            target.Method();

            callCountLog.Verify(l => l.Log(), Times.Exactly(2));
        }
        
        private class Target
        {
            [MethodCallCount]
            public void Method() { }
        }
    }
}
