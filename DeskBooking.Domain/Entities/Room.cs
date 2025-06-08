using DeskBooking.Domain.Entities.Base;
using DeskBooking.Domain.Enum.Rooms;

namespace DeskBooking.Domain.Entities;

public class Room : BaseEntity
{
    public required string RoomName { get; set; }
    
    public required string Description { get; set; }

    public RoomType RoomType { get; set; }

    public int? Capacity { get; set; }

    public Guid CoworkingId { get; set; }

    public Coworking? Coworking { get; set; }

    public List<OpenspaceDesk> OpenspaceDesks { get; set; } = [];

    public List<RoomAmenity> Amenities { get; set; } = [];

    public List<RoomPhoto> Photos { get; set; } = [];
}
