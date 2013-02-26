using System;
using System.Reflection;
using PostSharp.Aspects;

namespace Instrumentation.Aspects
{
    [Serializable]
    public class IntrumentationAspect : OnMethodBoundaryAspect
    {
        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            InstanceName = method.DeclaringType.FullName + "." + method.Name;
        }

        protected string InstanceName { get; private set; }
    }
}
