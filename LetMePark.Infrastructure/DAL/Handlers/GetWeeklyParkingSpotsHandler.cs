using LetMePark.Api.DTO;
using LetMePark.Application.Abstractions;
using LetMePark.Application.Queries;
using LetMePark.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LetMePark.Infrastructure.DAL.Handlers;

internal sealed class GetWeeklyParkingSpotsHandler : IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>>
{
    private readonly LetMeParkDbContext _context;

    public GetWeeklyParkingSpotsHandler(LetMeParkDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<WeeklyParkingSpotDto>> HandleAsync(GetWeeklyParkingSpots query)
    {
        var week = query.Date.HasValue ? new Week(query.Date.Value) : null;
        var weeklyParkingSpots = await _context.WeeklyParkingSpots
            .Where(x => week == null || x.Week == week)
            .Include(x => x.Reservations)
            .AsNoTracking()
            .ToListAsync();

        return weeklyParkingSpots.Select(x => x.AsDto());
    }
}