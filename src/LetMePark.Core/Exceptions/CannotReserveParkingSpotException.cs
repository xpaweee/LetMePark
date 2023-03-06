using LetMePark.Core.ValueObjects;

namespace LetMePark.Core.Exceptions;

public sealed class CannotReserveParkingSpotException : CustomException
{
    public ParkingSpotId ParkingSpotId { get; }

    public CannotReserveParkingSpotException(ParkingSpotId parkingSpotId ) : base($"Cannot reserve parking spot with ID: {parkingSpotId}")
    {
        ParkingSpotId = parkingSpotId;
    }
}