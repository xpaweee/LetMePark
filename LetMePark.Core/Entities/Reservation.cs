using LetMePark.Core.ValueObjects;

namespace LetMePark.Core.Entities;

public abstract class Reservation
{
    public ReservationId Id { get; private set; }
    public ParkingSpotId ParkingSpotId { get; private set; }
    public Date Date { get; private set; }

    public Capacity Capacity { get; private set; }

    protected Reservation()
    {
        
    }
    public Reservation(ReservationId id, ParkingSpotId parkingSpotId, Capacity capacity, Date date)
    {
        Id = id;
        ParkingSpotId = parkingSpotId;
        Date = date;
        Capacity = capacity;
    }

 
}