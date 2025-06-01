using DeskBooking.Domain.Enum.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Booking.EditBooking;

public class EditBookingCommand : IRequest
{
    public required string AnonUserId { get; set; }
    public Guid ReservationId { get; set; }
    public required int Capacity { get; set; }
    public RoomType RoomType { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
