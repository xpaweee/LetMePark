using LetMePark.Api.Exceptions;

namespace LetMePark.Api.Entities;

public class WeeklyParkingSpot
{
    public Guid Id { get; }
    public DateTime From { get; private set; }
    public DateTime To { get; private set; }
    public string Name { get; }
    private readonly HashSet<Reservation> _reservations = new();
    public HashSet<Reservation> Reservations => _reservations;


    public WeeklyParkingSpot(Guid id, DateTime from, DateTime to, string name)
    {
        Id = id;
        From = from;
        To = to;
        Name = name;
    }

    public void AddReservation(Reservation reservation)
    {
        var isInvalidDate = reservation.Date.Date < From || reservation.Date.Date > To ||
                            reservation.Date.Date < DateTime.UtcNow;
        if (isInvalidDate)
        {
            throw new InvalidReservationDateException(reservation.Date);
        }

        var reserveationAlreadyExists = Reservations.Any(x => x.Date.Date == reservation.Date.Date);

        if (reserveationAlreadyExists)
        {
            throw new ParkingSportAlreadyReservedException(reservation.Date, Name);
        }

        _reservations.Add(reservation);
    }
}