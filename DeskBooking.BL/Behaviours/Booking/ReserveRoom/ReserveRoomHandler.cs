using DeskBooking.BL.Services.Abstraction;
using DeskBooking.Domain.DatabaseConnection;
using DeskBooking.Domain.Entities;
using DeskBooking.Domain.Enum.Rooms;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Booking.ReserveRoom;

public class ReserveRoomHandler : IRequestHandler<ReserveRoomCommand>
{
    private readonly DataContext _context;
    private readonly IReservationJobService _reservationJobService; 

    public ReserveRoomHandler(DataContext context, IReservationJobService reservationJobService)
    {
        _context = context;
        _reservationJobService = reservationJobService;
    }

    public async Task Handle(ReserveRoomCommand request, CancellationToken cancellationToken)
    {
        var maxDuration = request.RoomType switch
        {
            RoomType.Openspace => TimeSpan.FromDays(30),
            RoomType.PrivateRoom => TimeSpan.FromDays(30),
            RoomType.MeetingRoom => TimeSpan.FromDays(1),
            _ => throw new RequestException(StatusCodes.Status400BadRequest, ErrorMessages.RoomNotFound)
        };

        if (request.EndDate <= request.StartDate)
        {
            throw new RequestException(StatusCodes.Status400BadRequest, ErrorMessages.WrongDateFormat);
        }

        if ((request.EndDate - request.StartDate) > maxDuration)
        {
            throw new RequestException(StatusCodes.Status400BadRequest,
                $"Максимальна тривалість бронювання для {request.RoomType} — {maxDuration.TotalDays} днів.");
        }

        var rooms = await _context.Rooms
            .Where(r => r.Capacity == request.Capacity && r.RoomType == request.RoomType)
            .ToListAsync(cancellationToken);

        var reservations = await _context.Reservations
            .Include(rs => rs.Room)
            .Where(rs => rs.Room.Capacity == request.Capacity && rs.Room.RoomType == request.RoomType)
            .ToListAsync(cancellationToken);

        var freeRooms = (
            from r in rooms
            join rs in reservations on r.Id equals rs.RoomId into roomRes
            from rs in roomRes.DefaultIfEmpty()
            select new { Room = r, Reservation = rs }
        ).Union(
            from rs in reservations
            join r in rooms on rs.RoomId equals r.Id into resRoom
            from r in resRoom.DefaultIfEmpty()
            select new { Room = r, Reservation = rs }
        ).ToList()
        .Where(group => group.Reservation is null || 
            ((request.StartDate > group.Reservation.EndTime && request.EndDate > group.Reservation.EndTime) ||
              request.EndDate < group.Reservation.StartTime && request.StartDate < group.Reservation.StartTime))
        .Select(group => group.Room)
        .ToList();


        if (freeRooms.Count == 0)
        {
            throw new RequestException(StatusCodes.Status400BadRequest, ErrorMessages.NoFreeRooms);
        }

        var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.AnonUserId == request.AnonUserId, cancellationToken);

        if (user is null)
        {
            throw new AuthException(StatusCodes.Status401Unauthorized, ErrorMessages.Status401UserNotAuthorized);
        }

        var reservation = new Reservation
        {
            AppUserId = user.Id,
            RoomId = freeRooms[0].Id,
            RoomType = freeRooms[0].RoomType,
            StartTime = request.StartDate,
            EndTime = request.EndDate,
            ScheduledJob = string.Empty
        };

        reservation.ScheduledJob = _reservationJobService.CreateJobForReservationDeleting(reservation);

        await _context.Reservations.AddAsync(reservation, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
