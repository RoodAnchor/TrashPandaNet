namespace TrashPandaNet.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggingMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<LoggingMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var message = $"[{DateTime.Now}]: http://{context.Request.Host.Value}{context.Request.Path}";

            _logger?.LogInformation(-1, message);

            await _next.Invoke(context);
        }
    }
}
