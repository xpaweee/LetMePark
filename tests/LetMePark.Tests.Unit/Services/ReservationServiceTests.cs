using LetMePark.Api.Commands;
using LetMePark.Api.Services;
using LetMePark.Core.Repository;
using LetMePark.Infrastructure.DAL.Repository;
using LetMePark.Tests.Unit.Shared;
using Shouldly;
using Xunit;

namespace LetMePark.Tests.Unit.Services;

public class ReservationServiceTests
{
    private readonly IReservationService _reservationService;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
    private readonly IClock _clock;
  
    
    
    public ReservationServiceTests()
    {
        _clock = new TestClock();
        _weeklyParkingSpotRepository = new InMemoryWeeklyParkingSpotRepository(_clock);
        _reservationService = new ReservationService(_clock, _weeklyParkingSpotRepository);
    }
    
    [Fact]
    public void given_reservation_for_not_taken_date_create_reservation_should_succeed()
    {
        var weeklyParkingSpot = _weeklyParkingSpotRepository.GetAll().First();
        var command = new CreateReservation(weeklyParkingSpot.Id, Guid.NewGuid(), "John Doe", "Test123",
            DateTime.UtcNow.AddMinutes(5));

        var reservationId = _reservationService.Create(command);

        reservationId.ShouldNotBeNull();
        reservationId.Value.ShouldBe(command.ReservationId);
    }
}