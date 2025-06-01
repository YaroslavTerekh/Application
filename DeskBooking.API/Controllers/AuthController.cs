using DeskBooking.BL.Behaviours.Auth.Register;
using DeskBooking.Domain.DatabaseConnection;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeskBooking.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody]RegisterCommand command, CancellationToken cancellationToken = default)
    {
        await _sender.Send(command, cancellationToken);

        return Created();
    }
}
