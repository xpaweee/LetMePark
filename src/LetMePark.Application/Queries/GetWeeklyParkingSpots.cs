using LetMePark.Api.DTO;
using LetMePark.Application.Abstractions;

namespace LetMePark.Application.Queries;

public class GetWeeklyParkingSpots : IQuery<IEnumerable<WeeklyParkingSpotDto>>
{
    public DateTime? Date { get; set; }
}