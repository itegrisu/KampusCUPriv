using Core.CrossCuttingConcern.Exceptions.Types;
using u = Core.CrossCuttingConcern.Exceptions.Types;

namespace Core.CrossCuttingConcern.Exceptions.WepApi.Handlers
{
    public abstract class ExceptionHandler
    {
        public abstract Task HandleException(BusinessException businessException);

        public abstract Task HandleException(u.ValidationException validationException);

        public abstract Task HandleException(AuthorizationException authorizationException);

        public abstract Task HandleException(NotFoundException notFoundException);

        public abstract Task HandleException(System.Exception exception);
    }
}
