using SpeakMore.Application.Shared.Domain.Models;

namespace SpeakMore.Application.Shared.Domain.Contracts
{
    public interface ICalculateCallValueService
    {
        Task<string> CalculateCallValueWithSpeakMorePlanAsync(CalculateCallValue calculateCallValue, CancellationToken cancellationToken);
        Task<string> CalculateCallValueWithOutSpeakMorePlanAsync(CalculateCallValue calculateCallValue, CancellationToken cancellationToken);
    }
}
