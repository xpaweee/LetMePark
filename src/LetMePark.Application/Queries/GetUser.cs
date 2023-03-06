using LetMePark.Api.DTO;
using LetMePark.Application.Abstractions;

namespace LetMePark.Application.Queries;

public class GetUser : IQuery<UserDto>
{
    public Guid UserId { get; set; }
}


public class GetUsers : IQuery<IEnumerable<UserDto>>
{
}
