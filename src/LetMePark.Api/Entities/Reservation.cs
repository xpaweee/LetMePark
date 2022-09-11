using LetMePark.Api.Exceptions;
using LetMePark.Api.ValueObjects;

namespace LetMePark.Api.Entities;

public class Reservation
{
    public ReservationId Id { get; }
    public ParkingSpotId ParkingSpotId { get; set; }
    public EmployeeName EmployeeName { get; private set; }
    public LicensePlate LicensePlate { get; private set; }
    public Date Date { get; private set; }

    public Reservation(ReservationId id, ParkingSpotId parkingSpotId, EmployeeName employeeName, LicensePlate licensePlate, Date date)
    {
        Id = id;
        EmployeeName = employeeName;
        ParkingSpotId = parkingSpotId;
        ChangeLicensePlate(licensePlate);
        Date = date;
    }

    public void ChangeLicensePlate(LicensePlate licensePlate)
    {
        LicensePlate = licensePlate;
    }
}