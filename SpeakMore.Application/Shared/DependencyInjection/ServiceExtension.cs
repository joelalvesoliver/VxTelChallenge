using Microsoft.Extensions.DependencyInjection;
using SpeakMore.Application.Shared.Domain.Contracts;
using SpeakMore.Application.Shared.Services;

namespace SpeakMore.Application.Shared.DependencyInjection
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServiceExtensions(this IServiceCollection services)
        {
            services.AddTransient<ICalculateCallValueService, CalculateCallValueService>();

            return services;
        }
    }
}
