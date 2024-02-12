using System.Diagnostics;
using System.Text;

namespace Products.Api.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            // To text file
            var logMessage = new StringBuilder()
                .AppendLine($"Request {context.Request.Path} received at {DateTime.UtcNow}");

            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                stopwatch.Stop();
                var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

                await responseBody.CopyToAsync(originalBodyStream);

                logMessage.AppendLine($"Executed in {elapsedMilliseconds} ms")
                    .AppendLine();
            }

            // Write to file
            var logFilePath = "request_logs.txt";
            await File.AppendAllTextAsync(logFilePath, logMessage.ToString());

            // Log also in App log
            _logger.LogInformation(logMessage.ToString());
        }
    }
}
