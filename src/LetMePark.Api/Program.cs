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
        .AddInfrastructure(builder.Configuration);
        

var app = builder.Build();

app.UseInfrastructure();

app.Run();
