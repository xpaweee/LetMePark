using LetMePark.Application.Abstractions;

namespace LetMePark.Api.Commands
{
    public record ReserveParkingSpotForVehicle(Guid ParkingSpotId, Guid ReservationId, int Capacity, string EmployeeName, string LicensePlate, DateTime Date) : ICommand;
}