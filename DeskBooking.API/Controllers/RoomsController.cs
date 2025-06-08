using DeskBooking.BL.Behaviours.Coworkings.GetAllCoworkings;
using DeskBooking.BL.Behaviours.Rooms.GetAllRooms;
using DeskBooking.BL.Behaviours.Rooms.GetRoomTypes;
using DeskBooking.BL.Behaviours.Rooms.GetSpecifiedRoomType;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeskBooking.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    private readonly ISender _sender;

    public RoomsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("coworkings/all")]
    public async Task<IActionResult> GetAllCoworkingsAsync(CancellationToken cancellationToken = default)
    {
        return Ok(await _sender.Send(new GetAllCoworkingsQuery(), cancellationToken));
    }

    [HttpGet("{id:guid}/all")]
    public async Task<IActionResult> GetAllRoomsAsync([FromRoute] Guid id,CancellationToken cancellationToken = default)
    {
        return Ok(await _sender.Send(new GetAllRoomsQuery(id), cancellationToken));
    }

    [HttpGet("types")]
    public async Task<IActionResult> GetAllRoomsTypesAsync(CancellationToken cancellationToken = default)
    {
        return Ok(await _sender.Send(new GetRoomTypesQuery(), cancellationToken));
    }

    [HttpPost("sorted")]
    public async Task<IActionResult> GetSelectedRoomAsync([FromBody] GetSpecifiedRoomTypeQuery  query, CancellationToken cancellationToken = default)
    {
        return Ok(await _sender.Send(query, cancellationToken));
    }
}
