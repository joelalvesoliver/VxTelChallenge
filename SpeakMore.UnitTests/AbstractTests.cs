using AutoFixture;
using Moq.AutoMock;
using SpeakMore.Application.Shared.Domain.Entities;

namespace SpeakMore.UnitTests
{
    public abstract class AbstractTests
    {
        public Fixture Fixture { get; }
        public AutoMocker Mocker { get; }

        public AbstractTests()
        {
            Fixture = new Fixture();
            Mocker = new AutoMocker();
        }

        public PhoneCallRate CreatePhoneCallRate()
        {
            return Fixture.Build<PhoneCallRate>().Create();
        }

        public PhonePlan CreatePhonePlan()
        {
            return Fixture.Build<PhonePlan>().Create();
        }
    }
}
