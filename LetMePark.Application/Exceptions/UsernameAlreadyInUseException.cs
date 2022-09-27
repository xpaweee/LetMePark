using LetMePark.Core.Exceptions;

namespace LetMePark.Application.Exceptions;

public sealed class UsernameAlreadyInUseException : CustomException
{
    public string Username { get; }

    public UsernameAlreadyInUseException(string username) : base($"Username: '{username}' is already in use.")
    {
        Username = username;
    }
}