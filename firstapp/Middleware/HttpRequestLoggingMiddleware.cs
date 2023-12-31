using Microsoft.Extensions.Logging;
using System.Text;

namespace firstapp.Middleware
{
    public class HttpRequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpRequestLoggingMiddleware> _logger;

        public HttpRequestLoggingMiddleware(RequestDelegate next, ILogger<HttpRequestLoggingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation($"Http Request information:{Environment.NewLine}" +
                                   $"Method: {context.Request.Method}{Environment.NewLine}" +
                                   $"Endpoint: {context.Request.Path}{Environment.NewLine}" +
                                   $"Body: {await GetResponseBodyContent(context.Request.Body)}{Environment.NewLine}" +
                                   $"User Id: {context.User?.Identity?.Name ?? "N/A"}{Environment.NewLine}");

            await _next(context);
        }

        private async Task<string> GetResponseBodyContent(Stream requestStream)
        {
            string bodyText = await new StreamReader(requestStream).ReadToEndAsync();
            return bodyText;
        }
    }

    // Middleware extension method
    public static class HttpRequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpRequestLoggingMiddleware>();
        }
    }
}
