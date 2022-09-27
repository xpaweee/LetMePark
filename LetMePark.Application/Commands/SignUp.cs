using LetMePark.Application.Abstractions;

namespace LetMePark.Api.Commands;

public record SignUp
    (Guid UserId, string Email, string Username, string Password, string FullName, string Role) : ICommand;
