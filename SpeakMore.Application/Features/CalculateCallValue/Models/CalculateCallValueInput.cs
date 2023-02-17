using MediatR;
using SpeakMore.Application.Shared.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SpeakMore.Application.Features.CalculateCallValue.Models
{
    [ExcludeFromCodeCoverage]
    public class CalculateCallValueInput : IRequest<Output<CalculateCallValueOutput>>
    {
        public int Origin { get; set; }
        public int Destination { get; set; }
        public int TimeOfCall { get; set; }
        public string PlanName { get; set; }
    }
}
