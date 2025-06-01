using DeskBooking.Domain.Entities;
using DeskBooking.Domain.Enum.Rooms;

namespace DeskBooking.Domain.DTOs.ResponseModels;

public class BookingDTO
{
    public Guid Id { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public required RoomDTO Room { get; set; }
    public required AppUserDTO AppUser { get; set; }
}
