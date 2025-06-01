using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Booking.ReserveDesk;

public class ReserveDeskCommand : IRequest
{
    public required string AnonUserId { get; set; }
    public Guid RoomId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
