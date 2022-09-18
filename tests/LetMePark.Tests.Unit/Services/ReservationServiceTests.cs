using LetMePark.Api.Commands;
using LetMePark.Api.Services;
using LetMePark.Core.Abstractions;
using LetMePark.Core.DomainServices;
using LetMePark.Core.Policies;
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

        var parkingReservationService = new ParkingReservationService(new IReservationPolicy[]
        {
            new BossReservationPolicy(),
            new ManagerReservationPolicy(),
            new RegularEmployeeReservationPolicy(_clock)
        }, _clock);
        _reservationService = new ReservationService(_clock, _weeklyParkingSpotRepository, parkingReservationService);
    }
    
    [Fact]
    public async Task given_reservation_for_not_taken_date_create_reservation_should_succeed()
    {
        var weeklyParkingSpot = (await _weeklyParkingSpotRepository.GetAllAsync()).First();
        var command = new ReserveParkingSpotForVehicle(weeklyParkingSpot.Id, Guid.NewGuid(), "John Doe", "Test123",
            DateTime.UtcNow.AddMinutes(5));

        var reservationId = await _reservationService.ReserveForVehicleAsync(command);

        reservationId.ShouldNotBeNull();
        reservationId.Value.ShouldBe(command.ReservationId);
    }
}