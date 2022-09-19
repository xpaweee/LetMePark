using LetMePark.Core.ValueObjects;

namespace LetMePark.Core.Exceptions;

public sealed class ParkingSpotCapacityExceededException : CustomException
{
    public ParkingSpotId ParkingSpotId { get; }

    public ParkingSpotCapacityExceededException(ParkingSpotId parkingSpotId) : base($"Parking spot with ID: {parkingSpotId} exceed its reservation capatiyty.")
    {
        ParkingSpotId = parkingSpotId;
    }
}