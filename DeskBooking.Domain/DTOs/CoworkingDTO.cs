using DeskBooking.Domain.Enum.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.DTOs;

public class CoworkingDTO
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required string Address { get; set; }

    public List<CoworkingRooms> AvailableRooms { get; set; } = [];

    public List<CoworkingPhotoDTO> Photos { get; set; } = [];
}

public class CoworkingRooms
{
    public RoomType RoomsType { get; set; }

    public int Quantity { get; set; }
}
