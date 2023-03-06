using LetMePark.Application.Abstractions;
using LetMePark.Core.ValueObjects;

namespace LetMePark.Api.Commands;

public record SignIn (string Email, string Password) : ICommand;