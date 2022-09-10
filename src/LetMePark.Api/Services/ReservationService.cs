using System.Resources;
using LetMePark.Api.Entities;

namespace LetMePark.Api.Services;

public class ReservationService
{
    private static readonly List<WeeklyParkingSpot> WeeklyParkingSpots = new()
    {
        new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P1"),
        new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P2"),
        new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P3"),
        new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P4"),
        new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P5"),
      
    };

   

    public Reservation Get(Guid id)
    {
        return GetAllWeekly().SingleOrDefault(x => x.Id == id);
    }

    public IEnumerable<Reservation> GetAllWeekly()
    {
        return WeeklyParkingSpots.SelectMany(x => x.Reservations);
    }

    public Guid? Create(Reservation reservation)
    {

        var weeklyParkingSpot = WeeklyParkingSpots.SingleOrDefault(x => x.Id == reservation.ParkingSpotId);
        if (weeklyParkingSpot is null)
        {
            return default;
        }

        reservation.Id = Guid.NewGuid();
        weeklyParkingSpot.AddReservation(reservation);
      

        return reservation.Id;
    }

    public bool Update(int id, Reservation reservation)
    {
        var exeistinReservation = Reservations.SingleOrDefault(x => x.Id == id);

        if (exeistinReservation is null)
        {
            return false;
        }
        
        if (exeistinReservation.Date <= DateTime.UtcNow)
        {
            return false;
        }
     

        exeistinReservation.LicensePlate = reservation.LicensePlate;

        return true;

    }
}