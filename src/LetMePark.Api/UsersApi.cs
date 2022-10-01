﻿using LetMePark.Api.Commands;
using LetMePark.Api.DTO;
using LetMePark.Application.Abstractions;
using LetMePark.Application.Queries;

namespace LetMePark.Api;

internal static class UsersApi
{
    private const string MeRoute = "me";
    public static WebApplication UseUsersApi(this WebApplication app)
    {
        
        app.MapGet("api/users/me", async (HttpContext context, IQueryHandler<GetUser, UserDto> handler) =>
        {
            var userDto = await handler.HandleAsync(new GetUser()
            {
                UserId = Guid.Parse(context.User.Identity?.Name ?? string.Empty)
            });
        
            return userDto is null ? Results.NotFound() : Results.Ok(userDto);
        }).RequireAuthorization().WithName(MeRoute);


        app.MapGet("api/users/{userId:guid}", async (Guid userId, IQueryHandler<GetUser, UserDto> handler) =>
        {
            var userDto = await handler.HandleAsync(new GetUser()
            {
                UserId = userId
            });
        
            return userDto is null ? Results.NotFound() : Results.Ok(userDto);
        }).RequireAuthorization("is-admin");

        app.MapPost("api/users", async (SignUp command, ICommandHandler<SignUp> handler) =>
        {
            command = command with { UserId = Guid.NewGuid() };
            await handler.HandleAsync(command);
            return Results.CreatedAtRoute(MeRoute);
        });

        return app;
    }
}