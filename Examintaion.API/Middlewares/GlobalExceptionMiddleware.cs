
using System.Text;
using Template.API.Response;

namespace Template.API.Middlewares
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                StringBuilder msg = new StringBuilder("Internal Server Error");
                switch (ex)
                {
                    case KeyNotFoundException:
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        msg.Clear();
                        msg.Append($"{ex.Message}");
                        break;

                    case ArgumentException:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        msg.Clear();
                        msg.Append($"{ex.Message}");
                        break;

                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        if (!string.IsNullOrEmpty(ex.Message))
                            msg.Clear();

                        msg.Append($"{ex.Message ?? ex.Message}");
                        break;

                }

                // Log the exception
                _logger.LogError(ex.Message, "An unhandled exception occurred while processing the request.");

                context.Response.ContentType = "application/json";
                var response = ApiResponse<object>.Error(context.Response.StatusCode, msg.ToString());

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
