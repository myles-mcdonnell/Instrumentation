using System;
using System.Diagnostics;
using Instrumentation.Logging.Contract;
using PostSharp.Aspects;

namespace Instrumentation.Aspects
{
    [Serializable]
    public class MethodExecutionTimeAttribute : IntrumentationAspect
    {
        static readonly Stopwatch Stopwatch = new Stopwatch();
       
        static MethodExecutionTimeAttribute()
        {
            Stopwatch.Start();
        }

        private IMethodExecutionTimeLog _log;

        public override void RuntimeInitialize(System.Reflection.MethodBase method)
        {
            _log = LogProvider.GetMethodExecutionTimeLog(InstanceName);
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            try
            {
                args.MethodExecutionTag = Stopwatch.ElapsedTicks;
                base.OnEntry(args);
            }
            catch
            {
                //We may want to log this exception but it is unlikely that we would want to allow an instrumentation exception 
                //to cause failure at the target of the aspect
            }
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            try
            {
                _log.Log( Stopwatch.ElapsedTicks - (long)args.MethodExecutionTag);

                base.OnExit(args);
            }
            catch
            {
                //We may want to log this exception but it is unlikely that we would want to allow an instrumentation exception 
                //to cause failure at the target of the aspect
            }
        }
    }
}
