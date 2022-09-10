using LetMePark.Api.Exceptions;

namespace LetMePark.Api.Entities;

public class Reservation
{
    public Guid Id { get; }
    public Guid ParkingSpotId { get; set; }
    public string EmployeeName { get; private set; }
    public string LicensePlate { get; private set; }
    public DateTime Date { get; private set; }

    public Reservation(Guid id, Guid parkingSpotId, string employeeName, string licensePlate, DateTime date)
    {
        Id = id;
        EmployeeName = employeeName;
        LicensePlate = licensePlate;
        Date = date;
        ParkingSpotId = parkingSpotId;
    }

    public void ChangeLicensePlate(string licensePlate)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
        {
            throw new EmptyLicensePlateException();
        }

        LicensePlate = licensePlate;
    }
}