using LetMePark.Core.Abstractions;
using LetMePark.Core.Entities;
using LetMePark.Core.Repository;
using LetMePark.Core.ValueObjects;

namespace LetMePark.Infrastructure.DAL.Repository;

internal class InMemoryWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
{
    private readonly IClock _clock;

    private readonly List<WeeklyParkingSpot> _weeklyParkingSpots;

    public InMemoryWeeklyParkingSpotRepository(IClock clock)
    {
        _clock = clock;
        _weeklyParkingSpots = new()
        {
            WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(_clock.Current()), "P5"),
            WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(_clock.Current()), "P4"),
            WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(_clock.Current()), "P3"),
            WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(_clock.Current()), "P2"),
            WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(_clock.Current()), "P1")
        };
    }

    public Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync()
        => Task.FromResult(_weeklyParkingSpots.AsEnumerable());

    public Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id)
        => Task.FromResult(_weeklyParkingSpots.SingleOrDefault(x => x.Id == id));

    public Task AddAsync(WeeklyParkingSpot weeklyParkingSpot)
    {
        _weeklyParkingSpots.Add(weeklyParkingSpot);
        return Task.CompletedTask;
    }
    public Task UpdateAsync(WeeklyParkingSpot weeklyParkingSpot)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(WeeklyParkingSpot weeklyParkingSpot)
    {
        _weeklyParkingSpots.Remove(weeklyParkingSpot);
        return Task.CompletedTask;
    }
}