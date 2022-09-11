using LetMePark.Api.Entities;
using LetMePark.Api.Services;
using LetMePark.Api.ValueObjects;

namespace LetMePark.Api.Repository;

public class InMemoryWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
{
    private readonly IClock _clock;

    private readonly List<WeeklyParkingSpot> _weeklyParkingSpots;

    public InMemoryWeeklyParkingSpotRepository(IClock clock)
    {
        _clock = clock;
        _weeklyParkingSpots = new()
        {

            new(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(_clock.Current()), "P5"),
            new(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(_clock.Current()), "P4"),
            new(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(_clock.Current()), "P3"),
            new(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(_clock.Current()), "P2"),
            new(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(_clock.Current()), "P1")
        };
    }

    public IEnumerable<WeeklyParkingSpot> GetAll()
        => _weeklyParkingSpots;

    public WeeklyParkingSpot Get(ParkingSpotId id)
        => _weeklyParkingSpots.SingleOrDefault(x => x.Id == id);

    public void Add(WeeklyParkingSpot weeklyParkingSpot)
        => _weeklyParkingSpots.Add(weeklyParkingSpot);

    public void Update(WeeklyParkingSpot weeklyParkingSpot)
    {
       
    }

    public void Delete(WeeklyParkingSpot weeklyParkingSpot)
        => _weeklyParkingSpots.Remove(weeklyParkingSpot);
}