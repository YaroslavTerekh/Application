using DeskBooking.BL.Behaviours.Booking.GetMyBookings;
using DeskBooking.BL.Services.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeskBooking.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssistantController : ControllerBase
{
    private readonly IGroqAIService _groqAIService;
    private readonly ISender _sender;

    public AssistantController(IGroqAIService groqAIService, ISender sender)
    {
        _groqAIService = groqAIService;
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetTestAsync([FromQuery] string question, [FromQuery] string anonUserId, CancellationToken cancellationToken = default)
    {
        var myBookings = await _sender.Send(new GetMyBookingsQuery(anonUserId), cancellationToken);
        var result = await _groqAIService.GenerateResponseAsync(
                question,
                JsonConvert.SerializeObject(myBookings),
                cancellationToken);

        return Ok(new {
            text = result
        });
    }
}
