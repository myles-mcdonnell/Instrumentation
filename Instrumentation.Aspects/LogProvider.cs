using System;
using System.Collections.Generic;
using Instrumentation.Logging.Contract;

namespace Instrumentation.Aspects
{
    //This is essentially providing IoC.  The alternative would be to use a container library (e.g. Ninject) but due to the nature of AOP this
    //would require the aspects to reference the container package and reference the kernel directly, which is an anti-pattern.
    public static class LogProvider
    {
        private static readonly KeyedInstanceFactoryCache<ICallCountLog> CallCountLogs = new KeyedInstanceFactoryCache<ICallCountLog>();
        private static readonly KeyedInstanceFactoryCache<IMethodExecutionTimeLog> MethodExecutionTimeLogs = new KeyedInstanceFactoryCache<IMethodExecutionTimeLog>();

        public static Func<string, ICallCountLog> CallCountLogFactory { set { CallCountLogs.InstanceFactory = value; } }
        public static Func<string, IMethodExecutionTimeLog> MethodExecutionTimeLogFactory { set { MethodExecutionTimeLogs.InstanceFactory = value; } }
       
        public static ICallCountLog GetCallCountLog(string instanceName)
        {
            return CallCountLogs.GetInstance(instanceName);
        }

        public static IMethodExecutionTimeLog GetMethodExecutionTimeLog(string instanceName)
        {
            return MethodExecutionTimeLogs.GetInstance(instanceName);
        }

        private class KeyedInstanceFactoryCache<TInstance>
        {
            private readonly object _initLock = new object();
            private readonly IDictionary<string, TInstance> _instances = new Dictionary<string, TInstance>();

            public Func<string, TInstance> InstanceFactory { private get; set; }

            public TInstance GetInstance(string key)
            {
                if (!_instances.ContainsKey(key))
                {
                    lock (_initLock)
                    {
                        if (!_instances.ContainsKey(key))
                        {
                            _instances.Add(key, InstanceFactory(key));
                        }
                    }
                }

                return _instances[key];
            }
        }
    }
}
