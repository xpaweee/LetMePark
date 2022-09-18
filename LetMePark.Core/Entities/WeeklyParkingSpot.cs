using LetMePark.Core.Exceptions;
using LetMePark.Core.ValueObjects;

namespace LetMePark.Core.Entities;

public class WeeklyParkingSpot
{
    public ParkingSpotId Id { get; private set; }
    public Week Week { get; private set; }
    public string Name { get; private set; }

    private readonly HashSet<Reservation> _reservations = new();
    public HashSet<Reservation> Reservations => _reservations;


    public WeeklyParkingSpot(ParkingSpotId id, Week week, string name)
    {
        Id = id;
        Week = week;
        Name = name;
    }

    internal void AddReservation(Reservation reservation, Date now)
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
    
    public void RemoveReservations(IEnumerable<Reservation> reservations)
        => _reservations.RemoveWhere(x => reservations.Any(r => r.Id == x.Id));
}