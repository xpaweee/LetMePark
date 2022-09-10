using LetMePark.Api.Entities;
using LetMePark.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LetMePark.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly ReservationService _reservationService = new();

    [HttpGet("{id:guid}")]
    public ActionResult<Reservation> Get(Guid id)
    {
        
        return Ok(_reservationService.GetAllWeekly());
    }
    
    public IActionResult Post(Reservation reservation)
    {
        var id = _reservationService.Create(reservation);
        if(id is null)
        {
            return BadRequest();
        }
        
        return CreatedAtAction(nameof(Get), new {id}, null);
    }
}