using LetMePark.Application;
using LetMePark.Application.Services;
using LetMePark.Core;
using LetMePark.Core.ValueObjects;
using LetMePark.Infrastructure;
using LetMePark.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services
        .AddCore()
        .AddApplication()
        .AddInfrastructure();
        

var app = builder.Build();

app.MapControllers();


using (var scope = app.Services.CreateScope())
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


app.Run();
