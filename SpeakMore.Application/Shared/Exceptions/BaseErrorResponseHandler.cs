using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace SpeakMore.Application.Shared.Exceptions
{
    [ExcludeFromCodeCoverage]
    public static class BaseErrorResponseHandler
    {

        public static string Serialize(string message, int statusCode = StatusCodes.Status500InternalServerError, LogLevel logLevel = LogLevel.Error, string details = null)
        {
            var baseErrorResponse = new BaseErrorResponse
            {
                Message = message,
                StatusCode = statusCode,
                LogLevel = logLevel,
                Details = details
            };
            return JsonConvert.SerializeObject(baseErrorResponse);
        }


        public static BaseErrorResponse Deserialize(string message)
        {
            try
            {
                return JsonConvert.DeserializeObject<BaseErrorResponse>(message);
            }
            catch
            {
                return new BaseErrorResponse
                {
                    Message = message,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    LogLevel = LogLevel.Error
                };
            }
        }

        public static void LogFromException(Exception exception, ILogger logger, object @event)
        {
            var errorResponse = Deserialize(exception.Message);
        }
    }
}
