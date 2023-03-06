using LetMePark.Core.Entities;
using LetMePark.Core.ValueObjects;

namespace LetMePark.Core.Repository;

public interface IUserRepository
{
    Task<User> GetByIdAsync(UserId id);
    Task<User> GetByEmailAsync(Email email);
    Task<User> GetByUsernameAsync(Username username);
    Task AddAsync(User user);
}