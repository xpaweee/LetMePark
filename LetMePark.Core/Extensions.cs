using LetMePark.Core.DomainServices;
using LetMePark.Core.Policies;
using Microsoft.Extensions.DependencyInjection;

namespace LetMePark.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<IReservationPolicy, RegularEmployeeReservationPolicy>();
            services.AddSingleton<IReservationPolicy, ManagerReservationPolicy>();
            services.AddSingleton<IReservationPolicy, BossReservationPolicy>();
            services.AddSingleton<IParkingReservationService, ParkingReservationService>();

            return services;
        }
    }
}
