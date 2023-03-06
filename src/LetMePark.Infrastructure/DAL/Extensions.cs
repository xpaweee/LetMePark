using LetMePark.Api.Commands;
using LetMePark.Application.Abstractions;
using LetMePark.Core.Repository;
using LetMePark.Infrastructure.DAL.Decorators;
using LetMePark.Infrastructure.DAL.Repository;
using LetMePark.Infrastructure.Logging;
using LetMePark.Infrastructure.Logging.Decorators;
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
            var section = configuration.GetSection(SectionName);
            services.Configure<PostgresOptions>(section);
            
            var options = configuration.GetOptions<PostgresOptions>(SectionName);
            services.AddDbContext<LetMeParkDbContext>(x => x.UseNpgsql(options.ConnectionString));
            services.AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();
            services.AddScoped<IUserRepository, PostgresUserRepository>();
            services.AddScoped<IUnitOfWork, PostgresUnitOfWork>();
            services.AddHostedService<DatabaseInitializer>();
            services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
            
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
               

            return services;
        }

      
    }
}
