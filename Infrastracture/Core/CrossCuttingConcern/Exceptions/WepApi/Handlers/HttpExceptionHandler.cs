using Core.CrossCuttingConcern.Exceptions.Types;
using Core.CrossCuttingConcern.Exceptions.WepApi.Extensions;
using Core.CrossCuttingConcern.Exceptions.WepApi.HttpProblemDetails;
using Microsoft.AspNetCore.Http;
using U = Core.CrossCuttingConcern.Exceptions.WepApi.HttpProblemDetails;
using A = Core.CrossCuttingConcern.Exceptions.Types;

namespace Core.CrossCuttingConcern.Exceptions.WepApi.Handlers
{

    public class HttpExceptionHandler : ExceptionHandler
    {
        private HttpResponse? _response;

        public HttpResponse Response
        {
            get
            {
                return _response ?? throw new NullReferenceException("_response");
            }
            set
            {
                _response = value;
            }
        }

        public override Task HandleException(BusinessException businessException)
        {
            Response.StatusCode = 400;
            string text = new BusinessProblemDetails(businessException.Message).ToJson();
            return Response.WriteAsync(text);
        }

        public override Task HandleException(A.ValidationException validationException)
        {
            Response.StatusCode = 400;
            string text = new U.ValidationProblemDetails(validationException.Errors).ToJson();
            return Response.WriteAsync(text);
        }

        public override Task HandleException(AuthorizationException authorizationException)
        {
            Response.StatusCode = 401;
            string text = new AuthorizationProblemDetails(authorizationException.Message).ToJson();
            return Response.WriteAsync(text);
        }

        public override Task HandleException(NotFoundException notFoundException)
        {
            Response.StatusCode = 404;
            string text = new NotFoundProblemDetails(notFoundException.Message).ToJson();
            return Response.WriteAsync(text);
        }

        public override Task HandleException(System.Exception exception)
        {
            Response.StatusCode = 500;
            string text = new InternalServerErrorProblemDetails(exception.Message).ToJson();
            return Response.WriteAsync(text);
        }
    }
}
