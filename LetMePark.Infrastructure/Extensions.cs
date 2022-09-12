using LetMePark.Api.Services;
using LetMePark.Application.Services;
using LetMePark.Infrastructure.DAL;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("LetMePark.Tests.Unit")]
namespace LetMePark.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddSingleton<IClock, Clock>()
                .AddPostgres();

            return services;
        }
    }
}
