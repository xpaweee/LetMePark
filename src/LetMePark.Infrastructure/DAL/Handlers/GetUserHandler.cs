using LetMePark.Api.DTO;
using LetMePark.Application.Abstractions;
using LetMePark.Application.Queries;
using LetMePark.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LetMePark.Infrastructure.DAL.Handlers;

internal sealed class GetUserHandler : IQueryHandler<GetUser, UserDto>
{
    private readonly LetMeParkDbContext _dbContext;
    
    public GetUserHandler(LetMeParkDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<UserDto> HandleAsync(GetUser query)
    {
        var userId = new UserId(query.UserId);
        var user = await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == userId);

        return user?.AsDto();
    }
}