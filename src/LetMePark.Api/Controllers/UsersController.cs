using LetMePark.Api.Commands;
using LetMePark.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace LetMePark.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ICommandHandler<SignUp> _singUpHandler;

    public UsersController(ICommandHandler<SignUp> singUpHandler)
    {
        _singUpHandler = singUpHandler;
    }
    
    [HttpPost]
    public async Task<ActionResult> Post(SignUp command)
    {
        command = command with { UserId = Guid.NewGuid() };
        await _singUpHandler.HandleAsync(command);

        return NoContent();
    }
}