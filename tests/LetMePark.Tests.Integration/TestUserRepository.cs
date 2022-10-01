using LetMePark.Core.Entities;
using LetMePark.Core.Repository;
using LetMePark.Core.ValueObjects;

namespace LetMePark.Tests.Integration;

public class TestUserRepository : IUserRepository
{
    private readonly List<User> _useres = new();

    public  Task<User> GetByIdAsync(UserId id)
        => Task.FromResult(_useres.SingleOrDefault(x => x.Id == id));

    public Task<User> GetByEmailAsync(Email email)
        => Task.FromResult(_useres.SingleOrDefault(x => x.Email == email));

    public Task<User> GetByUsernameAsync(Username username)
        => Task.FromResult(_useres.SingleOrDefault(x => x.Username == username));

    public Task AddAsync(User user)
    {
        _useres.Add(user);
        return Task.CompletedTask;
    }
}