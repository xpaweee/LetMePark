using LetMePark.Application.Abstractions;
using LetMePark.Application.Exceptions;
using LetMePark.Core.Abstractions;
using LetMePark.Core.DomainServices;
using LetMePark.Core.Entities;
using LetMePark.Core.Repository;
using LetMePark.Core.ValueObjects;

namespace LetMePark.Api.Commands.Handlers;

public sealed class ReserveParkingSpotForVehicleHandler : ICommandHandler<ReserveParkingSpotForVehicle>
{
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
    private readonly IParkingReservationService _parkingReservationService;
    private readonly IClock _clock;

    public ReserveParkingSpotForVehicleHandler(IWeeklyParkingSpotRepository weeklyParkingSpotRepository, IClock clock, IParkingReservationService parkingReservationService)
    {
        _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
        _clock = clock;
        _parkingReservationService = parkingReservationService;
    }
    
    public async Task HandleAsync(ReserveParkingSpotForVehicle command)
    {
        var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
        var week = new Week(_clock.Current());

        var weeklyParkingSpots = (await _weeklyParkingSpotRepository.GetByWeekAsync(week)).ToList();

        var parkingSpotToReserve = weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);
        if (parkingSpotToReserve is null)
        {
            throw new WeeklyParkingSpotNotFoundException(parkingSpotId);
        }

        var reservation = new VehicleReservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName,
            command.LicensePlate, command.Capacity, new Date(command.Date));

        _parkingReservationService.ReserveSpotForVehicle(weeklyParkingSpots, JobTitle.Employee, parkingSpotToReserve, reservation);
            
        await _weeklyParkingSpotRepository.UpdateAsync(parkingSpotToReserve);
    }
}