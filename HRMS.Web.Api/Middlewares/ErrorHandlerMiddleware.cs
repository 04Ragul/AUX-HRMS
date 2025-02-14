using HRMS.Shared.Wrapper;
using HRMS.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace HRMS.Web.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                HttpResponse response = context.Response;
                response.ContentType = "application/json";
                Result<string> responseModel = await Result<string>.FailAsync(error.Message);
                response.StatusCode = error switch
                {
                    ApiException => (int)HttpStatusCode.BadRequest,// custom application error
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,// not found error
                    _ => (int)HttpStatusCode.InternalServerError,// unhandled error
                };
                string result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
