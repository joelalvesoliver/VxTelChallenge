using Microsoft.Extensions.DependencyInjection;
using SpeakMore.Application.Shared.Domain.Contracts;
using SpeakMore.Application.Shared.Services;
using System.Diagnostics.CodeAnalysis;

namespace SpeakMore.Application.Shared.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtension
    {
        public static IServiceCollection AddServiceExtensions(this IServiceCollection services)
        {
            services.AddTransient<ICalculateCallValueService, CalculateCallValueService>();

            return services;
        }
    }
}
