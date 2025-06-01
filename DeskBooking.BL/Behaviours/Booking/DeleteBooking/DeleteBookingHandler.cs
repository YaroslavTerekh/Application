using DeskBooking.BL.Services.Abstraction;
using DeskBooking.Domain.DatabaseConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Booking.DeleteBooking;

public class DeleteBookingHandler : IRequestHandler<DeleteBookingCommand>
{
    private readonly DataContext _context;
    private readonly IReservationJobService _reservationJobService;

    public DeleteBookingHandler(DataContext context, IReservationJobService reservationJobService)
    {
        _context = context;
        _reservationJobService = reservationJobService;
    }

    public async Task Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        var reservation = await _context.Reservations.FirstOrDefaultAsync(rs => rs.Id == request.Id, cancellationToken);

        if(reservation is null)
        {
            throw new NotFoundException(ErrorMessages.BookingNotFound);
        }

        _reservationJobService.RemoveJobForReservationDeleting(reservation);

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
