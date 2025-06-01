using DeskBooking.BL.Services.Abstraction;
using DeskBooking.Domain.DatabaseConnection;
using DeskBooking.Domain.Entities;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Services.Realization;

public class ReservationJobService : IReservationJobService
{
    private readonly DataContext _context;

    public ReservationJobService(DataContext context)
    {
        _context = context;
    }

    public string CreateJobForReservationDeleting(Reservation reservation)
    {
        try
        {
            BackgroundJob.Delete(reservation.ScheduledJob);
        } catch { }

        return BackgroundJob.Schedule(() => DeleteReservation(reservation.Id), reservation.EndTime);
    }

    public void DeleteReservation(Guid reservationId)
    {
        var reservation = _context.Reservations.FirstOrDefaultAsync(r => r.Id == reservationId).GetAwaiter().GetResult();

        if (reservation is null)
            return;

        _context.Reservations.Remove(reservation);
        _context.SaveChangesAsync().GetAwaiter().GetResult();
    }

    public void RemoveJobForReservationDeleting(Reservation reservation)
    {
        try
        {
            BackgroundJob.Delete(reservation.ScheduledJob);
        }
        catch { }
    }
}
