using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using SpeakMore.Application.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using System.Text;

namespace SpeakMore.Application.Shared.Middlewares
{
    [ExcludeFromCodeCoverage]
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        /// <summary>
        /// </summary>
        /// <param name="requestDelegate"></param>
        /// <param name="logger"></param>
        public ExceptionHandlerMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var body = string.Empty;
            try
            {
                body = await ReadBodyAsync(context);
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, body);
            }
        }

        private static async Task<string> ReadBodyAsync(HttpContext context)
        {
            context.Request.EnableBuffering();

            using var reader = new StreamReader(
                context.Request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                bufferSize: 1024,
                leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            return body;
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, string body)
        {
            var requestParsed = FormatRequest(context.Request, body);
            context.Response.ContentType = MediaTypeNames.Application.Json;

            if (exception is ValidationException validationException)
            {
                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                var errorResponse = BaseErrorResponseHandler.Deserialize(exception.Message);
                errorResponse.StatusCode = context.Response.StatusCode;
                errorResponse.LogLevel = LogLevel.Warning;
                await context.Response.WriteAsync(validationException.Message);
            }
            else
            {
                var errorResponse = BaseErrorResponseHandler.Deserialize(exception.Message);
                context.Response.StatusCode = errorResponse.StatusCode;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
        }

        private string FormatRequest(HttpRequest httpRequest, string body)
        {
            return string.Format("{{\"body\":{0},\"path\":\"{1}\",\"query\":{2},\"headers\":{3}}}",
                                string.IsNullOrEmpty(body) ? "\"\"" : body,
                                httpRequest.Path,
                                ParseKeys(httpRequest.Query),
                                ParseKeys(httpRequest.Headers));
        }

        private string ParseKeys(IEnumerable<KeyValuePair<string, StringValues>> values)
        {

            return $"[{string.Join(",", values.Select(kvp => $"{{\"{kvp.Key}\":\"{kvp.Value}\"}}"))}]";
        }
    }
}
