using MediatR;
using Microsoft.Extensions.Logging;
using SpeakMore.Application.Features.CalculateCallValue.Models;
using SpeakMore.Application.Shared.Domain.Contracts;
using SpeakMore.Application.Shared.Domain.Models;

namespace SpeakMore.Application.Features.CalculateCallValue.UseCase
{
    public class CalculateCallValueUseCase : IRequestHandler<CalculateCallValueInput, Output<CalculateCallValueOutput>>
    {
        private readonly ILogger<CalculateCallValueUseCase> _logger;
        private readonly ICalculateCallValueService _calculateCallValueService;

        public CalculateCallValueUseCase(ICalculateCallValueService calculateCallValueService, 
                                         ILogger<CalculateCallValueUseCase> logger)
        {
            _calculateCallValueService = calculateCallValueService;
            _logger = logger;
        }

        public async Task<Output<CalculateCallValueOutput>> Handle(CalculateCallValueInput request, CancellationToken cancellationToken)
        {
            try
            {
                var calculateCallValue = new Shared.Domain.Models.CalculateCallValue
                {
                    Destination = request.Destination,
                    Origin = request.Origin,
                    PlanName = request.PlanName,
                    TimeOfCall = request.TimeOfCall,
                };
                var output = new CalculateCallValueOutput
                {
                    WithoutSpeakMore = await _calculateCallValueService.CalculateCallValueWithOutSpeakMorePlanAsync(calculateCallValue, cancellationToken),
                    WithSpeakMore = await _calculateCallValueService.CalculateCallValueWithSpeakMorePlanAsync(calculateCallValue, cancellationToken)
                };

                return new Output<CalculateCallValueOutput>(output);
            }catch (Exception ex)
            {
                _logger.LogError(ex, "[{Event}] - {Message}", nameof(CalculateCallValueUseCase), ex.Message);
                throw;
            }
        }
    }
}
