using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.DTOs;

public class OpenspaceDeskDTO
{
    public Guid Id { get; set; }

    public int DeskNumber { get; set; }

    public RoomDTO? Room { get; set; }
}
