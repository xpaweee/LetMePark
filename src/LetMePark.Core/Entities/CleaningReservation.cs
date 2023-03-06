using LetMePark.Core.ValueObjects;

namespace LetMePark.Core.Entities;

public sealed class CleaningReservation : Reservation
{
    private CleaningReservation()
    {
    }
    public CleaningReservation(ReservationId id, ParkingSpotId parkingSpotId, Date date ) 
        : base(id, parkingSpotId,2, date)
    {
    }
}