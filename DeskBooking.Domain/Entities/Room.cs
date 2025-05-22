using DeskBooking.Domain.Entities.Base;
using DeskBooking.Domain.Enum.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.Entities;

public class Room : BaseEntity
{
    public required string RoomName { get; set; }
    
    public required string Description { get; set; }

    public RoomType RoomType { get; set; }

    public int? Capacity { get; set; }

    public List<OpenspaceDesk> OpenspaceDesks { get; set; } = [];

    public List<RoomAmenity> Amenities { get; set; } = [];
}
