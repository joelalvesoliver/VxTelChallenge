using SpeakMore.Application.Shared.Context;
using SpeakMore.Application.Shared.Domain.Entities;

namespace SpeakMore.Application.Shared.Initializers
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            context.PhonePlans.Add(new PhonePlan{ Name = "FaleMais 30", Time = 30});
            context.PhonePlans.Add(new PhonePlan { Name = "FaleMais 60", Time = 60 });
            context.PhonePlans.Add(new PhonePlan { Name = "FaleMais 120", Time = 120 });

            context.PhoneCallRates.Add(new PhoneCallRate { Origin = 011, Destination = 016, Rate = 1.90M});
            context.PhoneCallRates.Add(new PhoneCallRate { Origin = 016, Destination = 011, Rate = 2.90M });
            context.PhoneCallRates.Add(new PhoneCallRate { Origin = 011, Destination = 017, Rate = 1.70M });
            context.PhoneCallRates.Add(new PhoneCallRate { Origin = 017, Destination = 011, Rate = 2.70M });
            context.PhoneCallRates.Add(new PhoneCallRate { Origin = 011, Destination = 018, Rate = 0.90M });
            context.PhoneCallRates.Add(new PhoneCallRate { Origin = 018, Destination = 011, Rate = 1.90M });

            context.SaveChanges();
        }
    }
}
