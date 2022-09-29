using LetMePark.Api.DTO;
using LetMePark.Application.Abstractions;
using LetMePark.Application.Queries;
using Microsoft.EntityFrameworkCore;

namespace LetMePark.Infrastructure.DAL.Handlers;

internal sealed class GetUsersHandler : IQueryHandler<GetUsers, IEnumerable<UserDto>>
{
    private readonly LetMeParkDbContext _dbContext;

    public GetUsersHandler(LetMeParkDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<UserDto>> HandleAsync(GetUsers query)
        => await _dbContext.Users
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
}