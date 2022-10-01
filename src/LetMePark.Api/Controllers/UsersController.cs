using LetMePark.Api.Commands;
using LetMePark.Api.DTO;
using LetMePark.Application.Abstractions;
using LetMePark.Application.Queries;
using LetMePark.Application.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LetMePark.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ICommandHandler<SignUp> _singUpHandler;
    private readonly IQueryHandler<GetUsers, IEnumerable<UserDto>> _getUsersHandler;
    private readonly IQueryHandler<GetUser, UserDto> _getUserHandler;
    private readonly ICommandHandler<SignIn> _signInHandler;
    private readonly ITokenStorage _tokenStorage;


    public UsersController(ICommandHandler<SignUp> singUpHandler, IQueryHandler<GetUser, UserDto> getUserHandler, IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersHandler , ICommandHandler<SignIn> signInHandler, ITokenStorage tokenStorage)
    {
        _singUpHandler = singUpHandler;
        _getUserHandler = getUserHandler;
        _getUsersHandler = getUsersHandler;
        _signInHandler = signInHandler;
        _tokenStorage = tokenStorage;
    }
    
    [HttpPost]
    public async Task<ActionResult> Post(SignUp command)
    {
        command = command with { UserId = Guid.NewGuid() };
        await _singUpHandler.HandleAsync(command);

        return NoContent();
    }

    [HttpGet("{userId:guid}")]
    [Authorize(Policy = "is-admin")]
    [SwaggerOperation("Get single by user ID if exists")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> Get(Guid userId)
    {
        var user = await _getUserHandler.HandleAsync(new GetUser { UserId = userId });
        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpGet]
    [Authorize(Policy = "is-admin")]
    public async Task<ActionResult<ActionResult<UserDto>>> Get([FromQuery] GetUsers query)
        => Ok(await _getUsersHandler.HandleAsync(query));
   

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> Get()
    {
        if (HttpContext.User.Identity?.Name is null)
            return NotFound();
        
        var userId =  Guid.Parse(HttpContext.User.Identity.Name);
        var user = await _getUserHandler.HandleAsync(new GetUser{UserId = userId});
        
        return user;
    }
    
    [HttpPost("sign-in")]
    public async Task<ActionResult<JwtDto>> Post(SignIn command)
    {
        await _signInHandler.HandleAsync(command);
        var jwt = _tokenStorage.Get();
        
        return Ok(jwt);
    }
    
  
}