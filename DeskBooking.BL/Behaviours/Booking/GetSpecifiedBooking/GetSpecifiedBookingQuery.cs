using DeskBooking.Domain.DTOs.ResponseModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Booking.GetSpecifiedBooking;

public record GetSpecifiedBookingQuery(Guid BookingId) : IRequest<BookingDTO>;
