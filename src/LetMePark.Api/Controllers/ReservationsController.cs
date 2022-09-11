using LetMePark.Api.Commands;
using LetMePark.Api.Entities;
using LetMePark.Api.Services;
using LetMePark.Api.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace LetMePark.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly ReservationService _reservationService;

    public ReservationsController(ReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet("{id:guid}")]
    public ActionResult<Reservation> Get(Guid id)
    {
        
        return Ok(_reservationService.GetAllWeekly());
    }
    
    [HttpPut]
    public IActionResult Post(CreateReservation command)
    {
        var id = _reservationService.Create(command with {ReservationId = Guid.NewGuid()});
        
        if(id is null)
        {
            return BadRequest();
        }
        
        return CreatedAtAction(nameof(Get), new {id}, null);
    } 
    
    [HttpPut("{id:guid}")]
    public IActionResult Put(Guid id, ChangeReservationLicensePlate command)
    {

        if (_reservationService.Update(command with { ReservationId = id }))
        {
            return NoContent();
        }

        return NotFound();
    }   
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (_reservationService.Delete(new DeleteReservation(id)))
        {
            return NoContent();
        }


        return NotFound();
    }
}