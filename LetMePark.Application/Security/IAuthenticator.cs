using LetMePark.Api.DTO;

namespace LetMePark.Application.Security;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, string role);
}