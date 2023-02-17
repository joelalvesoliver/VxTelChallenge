using Moq;
using SpeakMore.Application.Shared.Domain.Contracts;
using SpeakMore.Application.Shared.Domain.Entities;
using SpeakMore.Application.Shared.Domain.Models;
using SpeakMore.Application.Shared.Services;
using System.Threading.Tasks;
using Xunit;

namespace SpeakMore.UnitTests.Services
{
    public class CalculateCallValueServiceTests : AbstractTests
    {
        private readonly Mock<IPhoneCallRateRepository> _phoneCallRateRepository;
        private readonly Mock<IPhonePlanRepository> _phonePlanRepository;
        public CalculateCallValueServiceTests()
        {
            _phoneCallRateRepository = new Mock<IPhoneCallRateRepository>();
            _phonePlanRepository = new Mock<IPhonePlanRepository>();    
        }

        [Fact]
        public async Task CalculateCallValueService_ShouldBeACalculateCallValueWithSpeakMorePlanAndReturnZero()
        {
            _phonePlanRepository.Setup(s => s.GetPhonePlanByNameAsync(It.IsAny<string>(), default)).ReturnsAsync(new PhonePlan {Name = "FaleMais 30", Time = 30});
            _phoneCallRateRepository.Setup(s => s.GetPhoneCallRateByOriginAndDestinationAsync(It.IsAny<int>(), It.IsAny<int>(), default))
                                    .ReturnsAsync(new PhoneCallRate {Origin= 011, Destination = 017, Rate= 1.70M});

            var sut = new CalculateCallValueService(_phoneCallRateRepository.Object, _phonePlanRepository.Object);
            var result = await sut.CalculateCallValueWithSpeakMorePlanAsync(new CalculateCallValue {TimeOfCall = 30 }, default);

            Assert.NotNull(result);
            Assert.Equal("$ 0,00", result);
        }

        [Fact]
        public async Task CalculateCallValueService_ShouldBeACalculateCallValueWithSpeakMorePlanAndReturnRateExcedent()
        {
            _phonePlanRepository.Setup(s => s.GetPhonePlanByNameAsync(It.IsAny<string>(), default)).ReturnsAsync(new PhonePlan { Name = "FaleMais 60", Time = 60 });
            _phoneCallRateRepository.Setup(s => s.GetPhoneCallRateByOriginAndDestinationAsync(It.IsAny<int>(), It.IsAny<int>(), default))
                                    .ReturnsAsync(new PhoneCallRate { Origin = 011, Destination = 017, Rate = 1.70M });

            var sut = new CalculateCallValueService(_phoneCallRateRepository.Object, _phonePlanRepository.Object);
            var result = await sut.CalculateCallValueWithSpeakMorePlanAsync(new CalculateCallValue { TimeOfCall = 80 }, default);

            Assert.NotNull(result);
            Assert.Equal("$ 37,40", result);
        }

        [Fact]
        public async Task CalculateCallValueService_ShouldBeACalculateCallValueWithSpeakMorePlanAndReturnANotValue()
        {
            _phonePlanRepository.Setup(s => s.GetPhonePlanByNameAsync(It.IsAny<string>(), default)).ReturnsAsync(new PhonePlan { Name = "FaleMais 60", Time = 60 });
            _phoneCallRateRepository.Setup(s => s.GetPhoneCallRateByOriginAndDestinationAsync(It.IsAny<int>(), It.IsAny<int>(), default))
                                    .ReturnsAsync(default(PhoneCallRate));

            var sut = new CalculateCallValueService(_phoneCallRateRepository.Object, _phonePlanRepository.Object);
            var result = await sut.CalculateCallValueWithSpeakMorePlanAsync(new CalculateCallValue { TimeOfCall = 80 }, default);

            Assert.NotNull(result);
            Assert.Equal("-", result);
        }

        [Fact]
        public async Task CalculateCallValueService_ShouldBeACalculateCallValueWithOutSpeakMorePlanAndReturnValueOfCall()
        {
            _phonePlanRepository.Setup(s => s.GetPhonePlanByNameAsync(It.IsAny<string>(), default)).ReturnsAsync(new PhonePlan { Name = "FaleMais 60", Time = 60 });
            _phoneCallRateRepository.Setup(s => s.GetPhoneCallRateByOriginAndDestinationAsync(It.IsAny<int>(), It.IsAny<int>(), default))
                                    .ReturnsAsync(new PhoneCallRate { Origin = 011, Destination = 017, Rate = 1.70M });

            var sut = new CalculateCallValueService(_phoneCallRateRepository.Object, _phonePlanRepository.Object);
            var result = await sut.CalculateCallValueWithOutSpeakMorePlanAsync(new CalculateCallValue { TimeOfCall = 80 }, default);

            Assert.NotNull(result);
            Assert.Equal("$ 136,00", result);
        }

        [Fact]
        public async Task CalculateCallValueService_ShouldBeACalculateCallValueWithOutSpeakMorePlanAndReturnANotValue()
        {
            _phonePlanRepository.Setup(s => s.GetPhonePlanByNameAsync(It.IsAny<string>(), default)).ReturnsAsync(new PhonePlan { Name = "FaleMais 60", Time = 60 });
            _phoneCallRateRepository.Setup(s => s.GetPhoneCallRateByOriginAndDestinationAsync(It.IsAny<int>(), It.IsAny<int>(), default))
                                    .ReturnsAsync(default(PhoneCallRate));

            var sut = new CalculateCallValueService(_phoneCallRateRepository.Object, _phonePlanRepository.Object);
            var result = await sut.CalculateCallValueWithOutSpeakMorePlanAsync(new CalculateCallValue { TimeOfCall = 80 }, default);

            Assert.NotNull(result);
            Assert.Equal("-", result);
        }
    }
}
