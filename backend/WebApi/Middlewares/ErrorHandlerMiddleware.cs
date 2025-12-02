using Application.Exceptions;
using Application.Wrappers;
using Serilog;
using System.Net;

namespace WebApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Unhandled exception occurred");

                var response = context.Response;
                response.ContentType = "application/json";

                IDictionary<string, string[]> validationErrors = null;
                var statusCode = HttpStatusCode.InternalServerError;
                var message = "Unhandled exception occurred";
                string errorCode = null;
                object data = null;

                switch (error)
                {
                    case AuthException e when e.ErrorCode == "FORBIDDEN":
                        statusCode = HttpStatusCode.Forbidden; // 403
                        message = e.Message;
                        errorCode = e.ErrorCode;
                        break;

                    case AuthException e:
                        statusCode = HttpStatusCode.Unauthorized;
                        message = e.Message;
                        errorCode = e.ErrorCode;
                        break;

                    case ValidationException e:
                        statusCode = HttpStatusCode.BadRequest;
                        message = e.Message;           // always "Validation failed"
                        errorCode = e.ErrorCode;       // always "VALIDATION_ERROR"

                        if (e.Failures != null && e.Failures.Any())
                        {
                            validationErrors = e.Failures
                                .GroupBy(f => f.PropertyName)
                                .ToDictionary(
                                    g => g.Key,
                                    g => g.Select(f => f.ErrorMessage).ToArray()
                                );
                        }
                        break;

                    case NotFoundException e:
                        statusCode = HttpStatusCode.NotFound;
                        message = e.Message;
                        errorCode = e.ErrorCode;
                        break;

                    case AppException e:
                        statusCode = HttpStatusCode.BadRequest; // default, მერე გადავწერთ ქვემოთ
                        message = e.Message;
                        errorCode = e.ErrorCode;
                        data = e.DataObject; // აქედან წამოგაქვს UserInfo ან სხვა
                        break;
                }

                var responseModel = new Response<object>
                {
                    Succeeded = false,
                    Code = (int)statusCode,
                    Message = message,
                    Error = errorCode,
                    Data = null,
                    ValidationErrors = validationErrors ?? new Dictionary<string, string[]>()
                };

                response.StatusCode = (int)statusCode;
                await response.WriteAsJsonAsync(responseModel);
            }
            finally
            {
                await Log.CloseAndFlushAsync();
            }
        }
    }
}
