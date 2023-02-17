using Microsoft.Extensions.DependencyInjection;
using SpeakMore.Application.Shared.Context;
using SpeakMore.Application.Shared.Domain.Contracts;
using SpeakMore.Application.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SpeakMore.Application.Shared.DependencyInjection
{
    public static class DataBaseExtensions
    {
        public static IServiceCollection AddDataBaseExtensions(this IServiceCollection services) =>
               services
                   .AddRepositories()
                   .AddAplicationDbContext();

        private static IServiceCollection AddAplicationDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseInMemoryDatabase("Db_Configurations");
                });

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IPhoneCallRateRepository, PhoneCallRateRepository>();
            services.AddTransient<IPhonePlanRepository, PhonePlanRepository>();
            
            return services;
        }
    }
}
