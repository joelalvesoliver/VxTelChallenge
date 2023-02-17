using Microsoft.EntityFrameworkCore;
using SpeakMore.Application.Shared.Domain.Entities;

namespace SpeakMore.Application.Shared.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<PhoneCallRate> PhoneCallRates { get; set; }
        public DbSet<PhonePlan> PhonePlans { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
