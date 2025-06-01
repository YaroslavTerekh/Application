using DeskBooking.Domain.Enum.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.DTOs.ResponseModels;

public class SortedRoomDTO
{
    public Guid Id { get; set; }

    public required string RoomName { get; set; }

    public required string Description { get; set; }

    public RoomType RoomType { get; set; }

    public string? CapacityOptions { get;set; }

    public List<RoomPhotoDTO> Photos { get; set; } = [];

    public List<AmenityDTO> Amenities { get; set; } = [];

    public List<Avaliability> Avaliabilities { get; set; } = [];
}

public class Avaliability
{

    public int? Capacity { get; set; }

    public int? Quantity { get; set; }

    public List<OpenspaceDeskDTO> Desks { get; set; } = [];
}
