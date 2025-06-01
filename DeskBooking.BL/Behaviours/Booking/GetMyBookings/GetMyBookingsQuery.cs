using DeskBooking.Domain.DatabaseConnection;
using DeskBooking.Domain.DTOs.ResponseModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Booking.GetMyBookings;

public record GetMyBookingsQuery(string anonUserId) : IRequest<List<BookingDTO>>;
