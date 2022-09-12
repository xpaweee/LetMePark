using LetMePark.Api.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LetMePark.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IReservationService, ReservationService>();

            return services;
        }
      
    }
}
