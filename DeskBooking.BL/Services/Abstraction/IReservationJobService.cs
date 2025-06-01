using DeskBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Services.Abstraction;

public interface IReservationJobService
{
    public string CreateJobForReservationDeleting(Reservation reservation);

    public void RemoveJobForReservationDeleting(Reservation reservation);
}
