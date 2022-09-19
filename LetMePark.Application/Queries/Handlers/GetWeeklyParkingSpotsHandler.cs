using LetMePark.Api.DTO;
using LetMePark.Application.Abstractions;

namespace LetMePark.Application.Queries.Handlers;

public sealed class GetWeeklyParkingSpotsHandler : IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>>
{
    public Task<IEnumerable<WeeklyParkingSpotDto>> HandleAsync(GetWeeklyParkingSpots query)
    {
        throw new NotImplementedException();
    }
}