using DeskBooking.BL.Services.Abstraction;
using DeskBooking.Domain.DatabaseConnection;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Booking.EditBooking;

public class EditBookingHandler : IRequestHandler<EditBookingCommand>
{
    private readonly DataContext _context;
    private readonly IReservationJobService _reservationJobService;

    public EditBookingHandler(DataContext context, IReservationJobService reservationJobService)
    {
        _context = context;
        _reservationJobService = reservationJobService;
    }

    public async Task Handle(EditBookingCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.AppUsers
            .FirstOrDefaultAsync(u => u.AnonUserId == request.AnonUserId, cancellationToken);

        if (user is null)
            throw new AuthException(StatusCodes.Status401Unauthorized, ErrorMessages.Status401UserNotAuthorized);

        var reservation = await _context.Reservations
            .Include(r => r.Room)
            .FirstOrDefaultAsync(r => r.Id == request.ReservationId && r.AppUserId == user.Id, cancellationToken);

        if (reservation is null)
            throw new RequestException(StatusCodes.Status404NotFound, ErrorMessages.BookingNotFound);

        var rooms = await _context.Rooms
            .Where(r => r.Capacity == request.Capacity && r.RoomType == request.RoomType)
            .ToListAsync(cancellationToken);

        var overlappingReservations = await _context.Reservations
            .Where(r =>
                r.Id != reservation.Id &&
                r.Room.Capacity == request.Capacity &&
                r.Room.RoomType == request.RoomType &&
                ((request.StartDate < r.EndTime && request.EndDate > r.StartTime)))
            .ToListAsync(cancellationToken);

        var occupiedRoomIds = overlappingReservations.Select(r => r.RoomId).ToHashSet();

        var freeRoom = rooms.FirstOrDefault(r => !occupiedRoomIds.Contains(r.Id));

        if (freeRoom is null)
            throw new RequestException(StatusCodes.Status400BadRequest, ErrorMessages.NoFreeRooms);

        reservation.RoomId = freeRoom.Id;
        reservation.RoomType = freeRoom.RoomType;
        reservation.StartTime = request.StartDate;
        reservation.EndTime = request.EndDate;        
        reservation.ScheduledJob = _reservationJobService.CreateJobForReservationDeleting(reservation);

        await _context.SaveChangesAsync(cancellationToken);

    }

}
