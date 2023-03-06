using LetMePark.Core.Entities;
using LetMePark.Core.ValueObjects;

namespace LetMePark.Core.Repository;

public interface IWeeklyParkingSpotRepository
{
    Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync();
    Task<IEnumerable<WeeklyParkingSpot>> GetByWeekAsync(Week week) => throw new NotImplementedException();
    Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id);
    Task AddAsync(WeeklyParkingSpot weeklyParkingSpot);
    Task UpdateAsync(WeeklyParkingSpot weeklyParkingSpot);
    Task DeleteAsync(WeeklyParkingSpot weeklyParkingSpot);
}