using LetMePark.Api.Commands;
using LetMePark.Api.DTO;
using LetMePark.Application.Abstractions;
using LetMePark.Application.Queries;
using LetMePark.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LetMePark.Api.Controllers;

[ApiController]
[Route("parking-spot")]
public class ParkingSpotsController : ControllerBase
{
    private readonly ICommandHandler<ReserveParkingSpotForVehicle> _reserveParkingSpotsForVehicleHandler;
    private readonly ICommandHandler<ReserveParkingSpotForCleaning> _reserveParkingSpotsForCleaningHandler;
    private readonly ICommandHandler<ChangeReservationLicensePlate> _changeReservationLicencePlateHandler;
    private readonly ICommandHandler<DeleteReservation> _deleteReservationHandler;
    private readonly IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>> _getWeeklyParkingSpots;

    public ParkingSpotsController(ICommandHandler<ReserveParkingSpotForVehicle> reserveParkingSpotsForVehicleHandler,
        ICommandHandler<ReserveParkingSpotForCleaning> reserveParkingSpotsForCleaningHandler,
        ICommandHandler<ChangeReservationLicensePlate> changeReservationLicencePlateHandler,
        ICommandHandler<DeleteReservation> deleteReservationHandler, IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>> getWeeklyParkingSpots)
    {
        _reserveParkingSpotsForVehicleHandler = reserveParkingSpotsForVehicleHandler;
        _reserveParkingSpotsForCleaningHandler = reserveParkingSpotsForCleaningHandler;
        _changeReservationLicencePlateHandler = changeReservationLicencePlateHandler;
        _deleteReservationHandler = deleteReservationHandler;
        _getWeeklyParkingSpots = getWeeklyParkingSpots;
    }
    
    [HttpGet]
    public async Task<ActionResult<Reservation>> Get([FromQuery] GetWeeklyParkingSpots query)
        => Ok(await _getWeeklyParkingSpots.HandleAsync(query));
    
    
    [HttpPost("{parkingSpotId:guid}/reservations/vehicle")]
    public async Task<ActionResult> Post(Guid parkingSpotId, ReserveParkingSpotForVehicle command)
    {
        await _reserveParkingSpotsForVehicleHandler.HandleAsync(command with
        {
            ReservationId = Guid.NewGuid(),
            ParkingSpotId = parkingSpotId,
        });
        return NoContent();
    }

    [HttpPost("reservations/cleaning")]
    public async Task<ActionResult> Post(ReserveParkingSpotForCleaning command)
    {
        await _reserveParkingSpotsForCleaningHandler.HandleAsync(command);
        return NoContent();
    }
    
    [HttpPut("reservations/{reservationId:guid}")]
    public async Task<ActionResult> Put(Guid reservationId, ChangeReservationLicensePlate command)
    {
        await _changeReservationLicencePlateHandler.HandleAsync(command with {ReservationId = reservationId});
        return NoContent();
    }

    [HttpDelete("reservations/{reservationId:guid}")]
    public async Task<ActionResult> Delete(Guid reservationId)
    {
        await _deleteReservationHandler.HandleAsync(new DeleteReservation(reservationId));
        return NoContent();
    }

}