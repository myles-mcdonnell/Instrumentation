using System;
using Instrumentation.Logging.Contract;
using PostSharp.Aspects;

namespace Instrumentation.Aspects
{
    [Serializable]
    public class MethodCallCountAttribute : IntrumentationAspect
    {
        private ICallCountLog _log;

        public override void RuntimeInitialize(System.Reflection.MethodBase method)
        {
            _log = LogProvider.GetCallCountLog(InstanceName);
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            try
            {
                _log.Log();

                base.OnEntry(args);
            }
            catch
            {
                //We may want to log this exception but it is unlikely that we would want to allow an instrumentation exception 
                //to cause failure at the target of the aspect
            }
        }
    }
}
