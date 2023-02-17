using Microsoft.Extensions.DependencyInjection;
using SpeakMore.Application.Features.CalculateCallValue.UseCase;
using System.Reflection;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace SpeakMore.Application.Features.CalculateCallValue.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class CalculateCallValueExtensions
    {
        public static IServiceCollection AddCalculateCallValueExtensions(this IServiceCollection services) =>
    services
        .AddMediatRExtensions();

        private static IServiceCollection AddMediatRExtensions(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CalculateCallValueUseCase).GetTypeInfo().Assembly);
            return services;
        }
    }
}
