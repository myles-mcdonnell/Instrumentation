using System;
using System.Collections.Generic;
using Amib.Threading;
using Authorisation;
using Instrumentation.Aspects;
using Intrumentation.Logging.Volatile;

namespace DemoShell
{
    class Program
    {
        static void Main(string[] args)
        {
            LogProvider.CallCountLogFactory = instanceName => new CallCountLog(instanceName);
            LogProvider.MethodExecutionTimeLogFactory = instanceName => new ExecutionTimeLog(instanceName);
            
            Console.WriteLine("Hammering the auth. svc. with one million concurrent requests, please hold...");

            var authService = new AuthorisationService();

            var threadPool = new SmartThreadPool();
            var workItemResults = new List<IWaitableResult>();


            for (var i = 0; i < 1000; i++)
            {
                workItemResults.Add(threadPool.QueueWorkItem(() =>
                {
                    for (var y = 0; y < 1000; y++)
                        authService.Authorise(new AuthorisationRequest());
                }));
            }

            SmartThreadPool.WaitAll(workItemResults.ToArray());
        
            var authServiceAuthMethodName = string.Format("{0}.{1}", authService.GetType().FullName, "Authorise");

            Console.WriteLine("Auth. request count : {0}", LogProvider.GetCallCountLog(authServiceAuthMethodName).CallCount);
            Console.WriteLine("Avg. auth. request execution (ms) : {0}", LogProvider.GetMethodExecutionTimeLog(authServiceAuthMethodName).AverageExecutionTime());

#if DEBUG
            Console.ReadLine();
#endif
        }
    }
}
