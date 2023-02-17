using SpeakMore.Application.Shared.Domain.Contracts;
using SpeakMore.Application.Shared.Domain.Models;
using SpeakMore.Application.Shared.Exceptions;

namespace SpeakMore.Application.Shared.Services
{
    public class CalculateCallValueService : ICalculateCallValueService
    {
        private readonly IPhoneCallRateRepository _phoneCallRateRepository;
        private readonly IPhonePlanRepository _phonePlanRepository;

        public CalculateCallValueService(IPhoneCallRateRepository phoneCallRateRepository, 
                                         IPhonePlanRepository phonePlanRepository)
        {
            _phoneCallRateRepository = phoneCallRateRepository;
            _phonePlanRepository = phonePlanRepository;
        }

        public async Task<string> CalculateCallValueWithSpeakMorePlanAsync(CalculateCallValue calculateCallValue, CancellationToken cancellationToken)
        {
            var phonePlan = await _phonePlanRepository.GetPhonePlanByNameAsync(calculateCallValue.PlanName, cancellationToken);
            if (phonePlan == null)
                throw new InvalidRequestException("[CalculateCallValueService] - Invalid PlanName");

            var rate = await _phoneCallRateRepository.GetPhoneCallRateByOriginAndDestinationAsync(calculateCallValue.Origin, calculateCallValue.Destination, cancellationToken);

            if (rate == null)
                return "-";

            var differenceTimeUsed = calculateCallValue.TimeOfCall - phonePlan.Time;

            if (differenceTimeUsed < 0)
                return "$ " + 0.0M.ToString();
            else {
                var tenPercentoOfRate = rate.Rate * 0.1M;
                return "$ " + decimal.Round(differenceTimeUsed * (rate.Rate + tenPercentoOfRate), 2).ToString();
            }
        }

        public async Task<string> CalculateCallValueWithOutSpeakMorePlanAsync(CalculateCallValue calculateCallValue, CancellationToken cancellationToken)
        {
            var phonePlan = await _phonePlanRepository.GetPhonePlanByNameAsync(calculateCallValue.PlanName, cancellationToken);
            if (phonePlan == null)
                throw new InvalidRequestException("[CalculateCallValueService] - Invalid PlanName");

            var rate = await _phoneCallRateRepository.GetPhoneCallRateByOriginAndDestinationAsync(calculateCallValue.Origin, calculateCallValue.Destination, cancellationToken);

            if (rate == null)
                return "-";

            return "$ " + decimal.Round(calculateCallValue.TimeOfCall * rate.Rate, 2).ToString();
        }
    }
}
