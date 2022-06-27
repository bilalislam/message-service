using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security.Authentication;
using Newtonsoft.Json;

namespace MessageService.Api
{
    [ExcludeFromCodeCoverage]
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private const string ErrorMessage = "Üzgünüz! İşleminiz sırasında beklenmedik bir hata olustu.";

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(context, exception);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            string errorCode;
            HttpStatusCode statusCode;
            string message = exception.Message;

            switch (exception)
            {
                case ValidationException validationException:
                {
                    errorCode = validationException.Code;
                    switch (errorCode)
                    {
                        default:
                            statusCode = HttpStatusCode.BadRequest;
                            message = string.IsNullOrEmpty(validationException.UserFriendlyMessage) ? ErrorMessage : validationException.UserFriendlyMessage;
                            break;
                    }

                    break;
                }
                case BusinessException businessException:
                {
                    errorCode = businessException.Code;
                    switch (errorCode)
                    {
                        default:
                            statusCode = HttpStatusCode.BadRequest;
                            message = businessException.Message;
                            break;
                    }

                    break;
                }
                case AuthenticationException _:
                {
                    errorCode = "EUNAUTH1001";
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                }
                default:
                    errorCode = "EUN1001";
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }


            if (statusCode == HttpStatusCode.InternalServerError)
            {
                message = exception.ToString();
            }

            _logger.LogError(
                "[ERROR] Code(Business): {errorCode} StatusCode: {statusCode} Message: {message}", errorCode,
                statusCode, message);

            ErrorResponseDto responseModel = new ErrorResponseDto
            {
                Instance = $"urn:messagingservice:{statusCode}:{context.TraceIdentifier}",
                Messages = new List<MessageContractDto>
                {
                    new()
                    {
                        Code = errorCode,
                        Content =  exception is BusinessException || exception is ValidationException ? message : string.Empty,
                        Title = ErrorMessage
                    }
                },
                ReturnPath = context.Items.ContainsKey("returnPath") ? context.Items["returnPath"].ToString() : string.Empty
            };

            var content = JsonConvert.SerializeObject(responseModel);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(content);
        }
    }
}
