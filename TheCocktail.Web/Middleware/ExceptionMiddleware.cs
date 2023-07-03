using System.Net;
using TheCocktail.Application.Exceptions;
using TheCocktail.Web.Models;

namespace TheCocktail.Web.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                string errorId = Guid.NewGuid().ToString();

                var errorResult = new ErrorResult
                {
                    Source = exception.TargetSite?.DeclaringType?.FullName,
                    Exception = exception.Message.Trim(),
                    ErrorId = errorId,
                    SupportMessage = $"Provide the Error Id: {errorId} to the support team for further analysis."
                };
                errorResult.Messages.Add(exception.Message);

                if (exception is not CustomException && exception.InnerException != null)
                {
                    while (exception.InnerException != null)
                    {
                        exception = exception.InnerException;
                    }
                }

                switch (exception)
                {
                    case CustomException e:
                        _logger.LogError($"{nameof(CustomException)}: {exception.ToString()}");

                        errorResult.StatusCode = (int)e.StatusCode;
                        if (e.ErrorMessages is not null)
                        {
                            errorResult.Messages = e.ErrorMessages;
                        }
                        break;

                    default:
                        _logger.LogError($"{nameof(InternalServerException)}: {exception.ToString()}");

                        errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var response = context.Response;
                response.StatusCode = errorResult.StatusCode;

                context.Response.Redirect("/Home/Error");

            }
        }
    }
}
