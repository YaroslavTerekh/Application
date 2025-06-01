using DeskBooking.BL.Behaviours.Booking.DeleteBooking;
using DeskBooking.BL.Behaviours.Booking.EditBooking;
using DeskBooking.BL.Behaviours.Booking.GetMyBookings;
using DeskBooking.BL.Behaviours.Booking.GetSpecifiedBooking;
using DeskBooking.BL.Behaviours.Booking.ReserveDesk;
using DeskBooking.BL.Behaviours.Booking.ReserveRoom;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeskBooking.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly ISender _sender;

    public BookingController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("book/desk")]
    public async Task<IActionResult> BookDeskAsync([FromBody] ReserveDeskCommand command, CancellationToken cancellationToken = default)
    {
        await _sender.Send(command, cancellationToken);

        return Created();
    }

    [HttpPost("book/room")]
    public async Task<IActionResult> BookRoomAsync([FromBody] ReserveRoomCommand command, CancellationToken cancellationToken = default)
    {
        await _sender.Send(command, cancellationToken);

        return Created();
    }

    [HttpGet("get/{id:guid}")]
    public async Task<IActionResult> GetMyBookingsAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(new GetMyBookingsQuery(id.ToString()), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBookingAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(new GetSpecifiedBookingQuery(id), cancellationToken));


    [HttpPost("edit")]
    public async Task<IActionResult> EditRoomBookingAsync([FromBody] EditBookingCommand command, CancellationToken cancellationToken = default)
    {
        await _sender.Send(command, cancellationToken);

        return Ok();
    }

    [HttpDelete("delete/{id:guid}")]
    public async Task<IActionResult> DeleteBookingAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _sender.Send(new DeleteBookingCommand(id), cancellationToken);

        return Ok();
    }
}
