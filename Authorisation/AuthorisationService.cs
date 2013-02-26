using Instrumentation.Aspects;

namespace Authorisation
{
    public interface IAuthorisationService
    {
        void Authorise(AuthorisationRequest request);
    }

    public class AuthorisationService : IAuthorisationService
    {
        [MethodExecutionTime]
        [MethodCallCount]
        public void Authorise(AuthorisationRequest request)
        {
            //Authorisation code
        }
    }

    public class AuthorisationRequest
    {
    }
}
