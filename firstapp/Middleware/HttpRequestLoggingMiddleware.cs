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
            if (context.Request.Method == "POST")
            {
                if (context.Request.Body.CanSeek)
                {
                    context.Request.Body.Seek(0, SeekOrigin.Begin);
                    // Újra beolvassuk a testet
                    _logger.LogInformation($"Http Request information:{Environment.NewLine}" +
                                           $"Method: {context.Request.Method}{Environment.NewLine}" +
                                           $"Endpoint: {context.Request.Path}{Environment.NewLine}" +
                                           $"Body: {await GetResponseBodyContent(context.Request.Body)}{Environment.NewLine}" +
                                           $"User Id: {context.User?.Identity?.Name ?? "N/A"}{Environment.NewLine}");
                }
                else
                {
                    _logger.LogInformation($"Http Request information:{Environment.NewLine}" +
                                           $"Method: {context.Request.Method}{Environment.NewLine}" +
                                           $"Endpoint: {context.Request.Path}{Environment.NewLine}" +
                                           $"Body: Cannot re-read the request body due to its configuration.{Environment.NewLine}" +
                                           $"User Id: {context.User?.Identity?.Name ?? "N/A"}{Environment.NewLine}");
                }
            }
            else
            {
                _logger.LogInformation($"Http Request information:{Environment.NewLine}" +
                                       $"Method: {context.Request.Method}{Environment.NewLine}" +
                                       $"Endpoint: {context.Request.Path}{Environment.NewLine}" +
                                       $"User Id: {context.User?.Identity?.Name ?? "N/A"}{Environment.NewLine}");
            }

            await _next(context);
        }

        private async Task<string> GetResponseBodyContent(Stream requestStream)
        {
            //requestStream.Seek(0, SeekOrigin.Begin);

            string bodyText = await new StreamReader(requestStream).ReadToEndAsync();

            //requestStream.Seek(0, SeekOrigin.Begin);

            return bodyText;
        }
    }

    public static class HttpRequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpRequestLoggingMiddleware>();
        }
    }
}
