using DeskBooking.BL.Services.Abstraction;
using DeskBooking.Domain.DatabaseConnection;
using DeskBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Booking.ReserveDesk;

public class ReserveDeskHandler : IRequestHandler<ReserveDeskCommand>
{
    private readonly DataContext _context;
    private readonly IReservationJobService _reservationJobService;

    public ReserveDeskHandler(DataContext context, IReservationJobService reservationJobService)
    {
        _context = context;
        _reservationJobService = reservationJobService;
    }

    public async Task Handle(ReserveDeskCommand request, CancellationToken cancellationToken)
    {
        var room = await _context.Rooms
            .Include(r => r.OpenspaceDesks)
            .FirstOrDefaultAsync(r => r.Id == request.RoomId, cancellationToken);

        if (room is null)
        {
            throw new NotFoundException(ErrorMessages.RoomNotFound);
        }

        var roomDesks = room.OpenspaceDesks.Select(d => d.Id).ToList();

        var reservedDesks = await _context.Reservations
            .Where(r => r.RoomId == room.Id &&
                roomDesks.Contains(r.OpenspaceDeskId ?? Guid.Empty) &&
                r.StartTime < request.EndDate &&
                r.EndTime > request.StartDate)
            .Select(d => d.OpenspaceDeskId ?? Guid.Empty)
            .ToListAsync(cancellationToken);

        var freeDesks = roomDesks.Except(reservedDesks).ToList();

        if(freeDesks.Count == 0)
        {
            throw new RequestException(StatusCodes.Status400BadRequest, ErrorMessages.TypedRoomsNotFound);
        }

        var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.AnonUserId == request.AnonUserId, cancellationToken);

        if (user is null)
        {
            throw new AuthException(StatusCodes.Status401Unauthorized, ErrorMessages.Status401UserNotAuthorized);
        }

        var reservation = new Reservation
        {
            AppUserId = user.Id,
            RoomId = request.RoomId,
            RoomType = room.RoomType,
            OpenspaceDeskId = freeDesks.FirstOrDefault(),
            StartTime = request.StartDate,
            EndTime = request.EndDate,
            ScheduledJob = string.Empty
        };

        reservation.ScheduledJob = _reservationJobService.CreateJobForReservationDeleting(reservation);

        await _context.Reservations.AddAsync(reservation, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);        
    }
}
