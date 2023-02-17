using Microsoft.EntityFrameworkCore;
using SpeakMore.Application.Shared.Context;
using SpeakMore.Application.Shared.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SpeakMore.UnitTests.Repositories
{
    public class PhonePlanRepositoryTests : AbstractTests
    {
        private readonly ApplicationDbContext _context;
        public PhonePlanRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);
        }

        [Fact]
        public async Task PhonePlanRepository_ShouldBeReturnAPhonePlan()
        {
            var phonePlan = CreatePhonePlan();

            _context.PhonePlans.Add(phonePlan);
            await _context.SaveChangesAsync();

            var sut = new PhonePlanRepository(_context);

            var result = await sut.GetPhonePlanByNameAsync(phonePlan.Name, default);

            Assert.NotNull(result);
            Assert.Equal(phonePlan.Name, result.Name);
        }
    }
}
