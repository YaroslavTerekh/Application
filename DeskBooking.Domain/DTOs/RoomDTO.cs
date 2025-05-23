using DeskBooking.Domain.Enum.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.DTOs;

public class RoomDTO
{
    public Guid Id { get; set; }

    public required string RoomName { get; set; }

    public required string Description { get; set; }

    public RoomType RoomType { get; set; }

    public int? Capacity { get; set; }
}
