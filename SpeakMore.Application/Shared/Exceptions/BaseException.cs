using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace SpeakMore.Application.Shared.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class BaseException : Exception
    {

        public BaseException()
        {
        }


        public BaseException(string message)
            : base(BaseErrorResponseHandler.Serialize(message))
        {
        }

        public BaseException(string message, Exception innerException) : base(message, innerException)
        {
        }


        protected BaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BaseException(string message,
                             int statusCode,
                             LogLevel logLevel = LogLevel.Error,
                             string details = null)
            : base(BaseErrorResponseHandler.Serialize(message, statusCode, logLevel, details))
        {
        }
    }
}
