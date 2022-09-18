using LetMePark.Api.Services;
using LetMePark.Application.Services;
using LetMePark.Infrastructure.DAL;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using LetMePark.Core.Abstractions;
using LetMePark.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

[assembly: InternalsVisibleTo("LetMePark.Tests.Unit")]
namespace LetMePark.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSingleton<ExceptionMiddleware>()
                .AddSingleton<IClock, Clock>()
                .AddPostgres(configuration);

            return services;
        }

        public static WebApplication UseInfrastructure(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.MapControllers();

            return app;
        }
        
    }
}
