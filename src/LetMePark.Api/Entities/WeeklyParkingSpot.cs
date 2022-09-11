using LetMePark.Api.Exceptions;
using LetMePark.Api.ValueObjects;

namespace LetMePark.Api.Entities;

public class WeeklyParkingSpot
{
    public ParkingSpotId Id { get; }
    public Week Week { get; }
    public string Name { get; }
    
    private readonly HashSet<Reservation> _reservations = new();
    public HashSet<Reservation> Reservations => _reservations;


    public WeeklyParkingSpot(Guid id, Week week, string name)
    {
        Id = id;
        Week = week;
        Name = name;
    }

    public void AddReservation(Reservation reservation, Date now)
    {
        var isInvalidDate = reservation.Date < Week.From || reservation.Date > Week.To ||
                            reservation.Date < now;
        if (isInvalidDate)
        {
            throw new InvalidReservationDateException(reservation.Date.Value.Date);
        }

        var reserveationAlreadyExists = Reservations.Any(x => x.Date == reservation.Date);

        if (reserveationAlreadyExists)
        {
            throw new ParkingSportAlreadyReservedException(reservation.Date.Value.Date, Name);
        }

        _reservations.Add(reservation);
    }
    
  

    public void RemoveReservation(ReservationId id)
        => _reservations.RemoveWhere(x => x.Id == id);
}