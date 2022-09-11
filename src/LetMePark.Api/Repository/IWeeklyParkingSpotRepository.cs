using LetMePark.Api.Entities;
using LetMePark.Api.ValueObjects;

namespace LetMePark.Api.Repository;

public interface IWeeklyParkingSpotRepository
{
     IEnumerable<WeeklyParkingSpot> GetAll(); 
     WeeklyParkingSpot Get(ParkingSpotId id);
     void Add(WeeklyParkingSpot weeklyParkingSpot);
     void Update(WeeklyParkingSpot weeklyParkingSpot);
     void Delete(WeeklyParkingSpot weeklyParkingSpot);
}