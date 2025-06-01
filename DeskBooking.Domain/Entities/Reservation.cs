using DeskBooking.Domain.Entities.Base;
using DeskBooking.Domain.Enum.Rooms;

namespace DeskBooking.Domain.Entities;

public class Reservation : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public Guid RoomId { get; set; }
    public Room? Room { get; set; }

    public required string ScheduledJob { get; set; }
    public RoomType RoomType { get; set; }
    public Guid? OpenspaceDeskId { get; set; }
}
