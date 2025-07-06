using Microsoft.AspNetCore.Diagnostics;

namespace PharmaDrop.API.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var response = new ErrorResponse
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = exception.Message,
                Title = "Something Wrong"
            };

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    }
}
