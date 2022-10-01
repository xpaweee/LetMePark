using LetMePark.Api;
using LetMePark.Api.Commands;
using LetMePark.Api.DTO;
using LetMePark.Application;
using LetMePark.Application.Abstractions;
using LetMePark.Application.Queries;
using LetMePark.Application.Services;
using LetMePark.Core;
using LetMePark.Core.ValueObjects;
using LetMePark.Infrastructure;
using LetMePark.Infrastructure.DAL;
using LetMePark.Infrastructure.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services
        .AddCore()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

builder.UseSerilog();

var app = builder.Build();

app.UseInfrastructure();

app.MapGet("api", (HttpContent ctx, IOptions<AppOptions> options) => Results.Ok(options.Value.Name));

app.UseUsersApi();

app.Run();
