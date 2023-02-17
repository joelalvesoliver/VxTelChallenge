using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SpeakMore.Application.Shared.Exceptions
{
    public class InvalidRequestException : BaseException
    {
        private const string ERROR_MESSAGE = "Invalid request!!";
        private const int STATUS_CODE = StatusCodes.Status400BadRequest;
        private const LogLevel LOG_LEVEL = LogLevel.Error;

        public InvalidRequestException(string message)
                    : base($"[{nameof(InvalidRequestException)}] {message}", STATUS_CODE, LOG_LEVEL)
        {
        }
    }
}
