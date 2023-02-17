using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SpeakMore.Application.Shared.Exceptions
{
    public class BaseErrorResponse
    {

        [JsonProperty(PropertyName = "message", Required = Required.Always)]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "statusCode", Required = Required.Always)]
        public int StatusCode { get; set; }

        [JsonProperty(PropertyName = "logLevel", Required = Required.Always)]
        public LogLevel LogLevel { get; set; }

        [JsonProperty(PropertyName = "details", NullValueHandling = NullValueHandling.Ignore)]
        public string Details { get; set; }
    }
}
