using System.Collections;
using LetMePark.Api.Entities;
using LetMePark.Api.Repository;
using LetMePark.Api.Services;
using LetMePark.Api.ValueObjects;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services
        .AddScoped<IClock, Clock>()
        .AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>()
        .AddSingleton<IReservationService, ReservationService>();
        

var app = builder.Build();

app.MapControllers();

app.Run();
