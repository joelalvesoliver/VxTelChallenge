using SpeakMore.Application.Shared.Domain.Entities;

namespace SpeakMore.Application.Shared.Domain.Contracts
{
    public interface IPhonePlanRepository
    {
        Task<PhonePlan> GetPhonePlanByNameAsync(string name, CancellationToken cancellationToken);
    }
}
