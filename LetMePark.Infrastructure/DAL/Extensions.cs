using LetMePark.Api.Commands;
using LetMePark.Application.Abstractions;
using LetMePark.Core.Repository;
using LetMePark.Infrastructure.DAL.Decorators;
using LetMePark.Infrastructure.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LetMePark.Infrastructure.DAL
{
    internal static class Extensions
    {
        private const string SectionName = "postgres";
        public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            var options = GetOptions<PostgresOptions>(configuration, SectionName);
            services.AddDbContext<LetMeParkDbContext>(x => x.UseNpgsql(options.ConnectionString));
            services.AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();
            services.AddScoped<IUnitOfWork, PostgresUnitOfWork>();
            services.AddHostedService<DatabaseInitializer>();
            services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
            services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
            
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
               

            return services;
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var options = new T();
            var section = configuration.GetSection(sectionName);
            section.Bind(options);

            return options;
        }
    }
}
