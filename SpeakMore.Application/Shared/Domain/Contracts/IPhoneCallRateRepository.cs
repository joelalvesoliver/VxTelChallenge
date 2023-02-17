using SpeakMore.Application.Shared.Domain.Entities;

namespace SpeakMore.Application.Shared.Domain.Contracts
{
    public interface IPhoneCallRateRepository
    {
        Task<PhoneCallRate> GetPhoneCallRateByOriginAndDestinationAsync(int origin, int destination, CancellationToken cancellationToken);
    }
}
