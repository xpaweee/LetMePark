using LetMePark.Core.Entities;
using LetMePark.Core.Exceptions;
using LetMePark.Core.ValueObjects;
using Shouldly;
using Xunit;

namespace LetMePark.Tests.Unit.Entities;

public class WeeklyParkingSpotTests
{
    private readonly WeeklyParkingSpot _weeklyParkingSpot;
    private readonly Date _now;
 
    
    public WeeklyParkingSpotTests()
    {
        _now = new Date(new DateTime(2022, 08,10));
        _weeklyParkingSpot =  WeeklyParkingSpot.Create(Guid.NewGuid(), new Week(_now), "P1");
    }

    [Theory]
    [InlineData("2022-08-09")]
    [InlineData("2022-09-12")]
    public void given_invalid_date_add_reservation_should_fail(string dateString)
    {
        var invalidDate = DateTime.Parse(dateString);
        var reservation = new VehicleReservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "Test123", 1,
            new Date(invalidDate));

        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, _now));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidReservationDateException>();
    }

    [Fact]
    public void given_reservation_for_already_reserved_parking_spot_add_reservation_should_fail()
    {
        var reservationDate = _now.AddDays(1);
        var reservation =
            new VehicleReservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "TEST123",2, reservationDate);
        _weeklyParkingSpot.AddReservation(reservation, _now);   
        var nextReservation =
            new VehicleReservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "TEST123", 1,reservationDate);
        _weeklyParkingSpot.AddReservation(reservation, _now);
        _weeklyParkingSpot.AddReservation(nextReservation, _now);   
        
        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, reservationDate));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ParkingSpotCapacityExceededException>();
    }
    
    [Fact]
    public void given_reservation_for_not_reserved_parking_spot_add_reservation_should_succeed()
    {
        var reservationDate = _now.AddDays(1);
        var reservation =
            new VehicleReservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "TEST123",2, reservationDate);
        
        _weeklyParkingSpot.AddReservation(reservation, _now);

        _weeklyParkingSpot.Reservations.ShouldHaveSingleItem();
    }
}