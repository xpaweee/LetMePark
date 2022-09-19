using System.Resources;
using LetMePark.Api.Commands;
using LetMePark.Api.Services;
using LetMePark.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LetMePark.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Reservation>> Get(Guid id)
    {

        var reservation = await _reservationService.GetAsync(id);
        if (reservation is null)
        {
            return NotFound();
        }

        return Ok(reservation);
    } 
    
    [HttpGet]
    public async Task<ActionResult<Reservation>> Get()
    {

        return Ok(await _reservationService.GetAllWeeklyAsync());
    }
   
    
    [HttpPost("vehicle")]
    public async Task<IActionResult> Post(ReserveParkingSpotForVehicle command)
    {
        var id = await _reservationService.ReserveForVehicleAsync(command with {ReservationId = Guid.NewGuid()});
        
        if(id is null)
        {
            return BadRequest();
        }
        
        return CreatedAtAction(nameof(Get), new {command.ReservationId}, null);
    }  
    
    [HttpPost("cleaning")]
    public async Task<IActionResult> Post(ReserveParkingSpotForCleaning command)
    {
        await _reservationService.ReserveForCleaningAsync(command);

        return Ok();
    } 
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, ChangeReservationLicensePlate command)
    {

        if (await _reservationService.ChangeReservationLicensePlateAsync(command with { ReservationId = id }))
        {
            return NoContent();
        }

        return NotFound();
    }   
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (await _reservationService.DeleteAsync(new DeleteReservation(id)))
        {
            return NoContent();
        }


        return NotFound();
    }
}