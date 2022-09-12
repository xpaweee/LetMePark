using LetMePark.Core.Repository;
using LetMePark.Infrastructure.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LetMePark.Infrastructure.DAL
{
    internal static class Extensions
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services)
        {
            const string connectionString = "Host=localhost;Database=LetMePark;Username=postgres;Password=admin";

            services.AddDbContext<LetMeParkDbContext>(x => x.UseNpgsql(connectionString));
            services.AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
               

            return services;
        }
    }
}
