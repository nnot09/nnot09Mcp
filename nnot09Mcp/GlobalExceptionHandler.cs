using Microsoft.AspNetCore.Diagnostics;

namespace nnot09Mcp
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            this._logger = logger;
        }

        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {   
            _logger.LogCritical(exception, "Something went wrong.");
            httpContext.Response.StatusCode = 500;
            return ValueTask.FromResult(true);
        }
    }
}
