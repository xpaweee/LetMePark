using LetMePark.Application.Services;
using LetMePark.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LetMePark.Infrastructure.DAL;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbcontext = scope.ServiceProvider.GetRequiredService<LetMeParkDbContext>();
            dbcontext.Database.Migrate();

            var weeklyParkingSpots = dbcontext.WeeklyParkingSpots.ToList();
            if(!weeklyParkingSpots.Any())
            {
                var clock = new Clock();
                weeklyParkingSpots = new List<LetMePark.Core.Entities.WeeklyParkingSpot>()
                {
                    new(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(clock.Current()), "P5"),
                    new(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(clock.Current()), "P4"),
                    new(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(clock.Current()), "P3"),
                    new(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(clock.Current()), "P2"),
                    new(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(clock.Current()), "P1")
                };

                dbcontext.WeeklyParkingSpots.AddRange(weeklyParkingSpots);
                dbcontext.SaveChanges();
            }
        }
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}