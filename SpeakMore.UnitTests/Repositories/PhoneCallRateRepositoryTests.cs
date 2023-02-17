using Microsoft.EntityFrameworkCore;
using SpeakMore.Application.Shared.Context;
using SpeakMore.Application.Shared.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SpeakMore.UnitTests.Repositories
{
    public class PhoneCallRateRepositoryTests : AbstractTests
    {
        private readonly ApplicationDbContext _context;
        public PhoneCallRateRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
        }

        [Fact]
        public async Task PhoneCallRateRepository_ShouldBeReturnAPhoneCallRate()
        {
            var phoneCallRate = CreatePhoneCallRate();

            _context.PhoneCallRates.Add(phoneCallRate);
            await _context.SaveChangesAsync();

            var sut = new PhoneCallRateRepository(_context);

            var result = await sut.GetPhoneCallRateByOriginAndDestinationAsync(phoneCallRate.Origin,phoneCallRate.Destination, default);

            Assert.NotNull(result);
            Assert.Equal(phoneCallRate.Origin, result.Origin);
            Assert.Equal(phoneCallRate.Destination, result.Destination);
        }
    }
}
