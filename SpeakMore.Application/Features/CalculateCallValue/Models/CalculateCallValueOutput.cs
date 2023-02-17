using System.Diagnostics.CodeAnalysis;

namespace SpeakMore.Application.Features.CalculateCallValue.Models
{
    [ExcludeFromCodeCoverage]
    public class CalculateCallValueOutput
    {
        public string WithSpeakMore { get; set; }
        public string WithoutSpeakMore { get; set; }
    }
}
