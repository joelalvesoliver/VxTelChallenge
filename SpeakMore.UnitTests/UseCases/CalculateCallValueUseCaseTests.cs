using Microsoft.Extensions.Logging;
using Moq;
using SpeakMore.Application.Features.CalculateCallValue.Models;
using SpeakMore.Application.Features.CalculateCallValue.UseCase;
using SpeakMore.Application.Shared.Domain.Contracts;
using SpeakMore.Application.Shared.Domain.Models;
using System.Threading.Tasks;
using Xunit;

namespace SpeakMore.UnitTests.UseCases
{
    public class CalculateCallValueUseCaseTests
    {
        private readonly Mock<ICalculateCallValueService> _calculateCallValueService;
        private readonly Mock<ILogger<CalculateCallValueUseCase>> _logger;

        public CalculateCallValueUseCaseTests()
        {
            _calculateCallValueService = new Mock<ICalculateCallValueService>();
            _logger = new Mock<ILogger<CalculateCallValueUseCase>>();
        }

        [Fact]
        public async Task Handle_ShouldBeACalculateCallValue()
        {
            _calculateCallValueService.Setup(s => s.CalculateCallValueWithSpeakMorePlanAsync(It.IsAny<CalculateCallValue>(), default)).ReturnsAsync("$ -");
            _calculateCallValueService.Setup(s => s.CalculateCallValueWithOutSpeakMorePlanAsync(It.IsAny<CalculateCallValue>(), default)).ReturnsAsync("$ -");
            var sut = new CalculateCallValueUseCase(_calculateCallValueService.Object, _logger.Object);
            var result = await sut.Handle(new CalculateCallValueInput { TimeOfCall = 30 }, default);

            Assert.NotNull(result);
            Assert.Equal("$ -", result.Data.WithSpeakMore);
            Assert.Equal("$ -", result.Data.WithoutSpeakMore);
        }
    }
}
