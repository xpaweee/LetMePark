using LetMePark.Core.Entities;
using LetMePark.Core.ValueObjects;

namespace LetMePark.Core.Repository;

public interface IWeeklyParkingSpotRepository
{
    IEnumerable<WeeklyParkingSpot> GetAll();
    WeeklyParkingSpot Get(ParkingSpotId id);
    void Add(WeeklyParkingSpot weeklyParkingSpot);
    void Update(WeeklyParkingSpot weeklyParkingSpot);
    void Delete(WeeklyParkingSpot weeklyParkingSpot);
}