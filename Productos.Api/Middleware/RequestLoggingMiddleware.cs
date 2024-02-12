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

            // Buffer the response
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                stopwatch.Stop();
                var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

                // Intercept the response
                responseBody.Seek(0, SeekOrigin.Begin);
                var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();

                // Log request and response
                LogRequestAndResponse(context, elapsedMilliseconds, responseBodyText);

                // Restore the original response body and write the buffered response to the original stream
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private void LogRequestAndResponse(HttpContext context, long elapsedMilliseconds, string responseBodyText)
        {
            var logMessage = new StringBuilder()
                .AppendLine($"Request {context.Request.Path} received at {DateTime.UtcNow}")
                .AppendLine($"Executed in {elapsedMilliseconds} ms")
                .AppendLine($"Response body: {responseBodyText}")
                .AppendLine();

            // Write to file
            var logFilePath = "request_logs.txt";
            File.AppendAllText(logFilePath, logMessage.ToString());

            // Log also in App log
            _logger.LogInformation(logMessage.ToString());
        }
    }
}
