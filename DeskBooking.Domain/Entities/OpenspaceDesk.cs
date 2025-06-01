using DeskBooking.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.Entities;

public class OpenspaceDesk : BaseEntity
{
    public int DeskNumber { get; set; }

    public Guid RoomId { get; set; }

    public Room? Room { get; set; }
}
