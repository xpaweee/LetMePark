using LetMePark.Api.DTO;

namespace LetMePark.Application.Security;

public interface ITokenStorage
{
    void Set(JwtDto jwt);
    JwtDto Get();
}