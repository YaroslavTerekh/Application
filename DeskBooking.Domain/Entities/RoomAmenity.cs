using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.Entities;

public class RoomAmenity
{
    public Guid RoomId { get; set; }
    public Room? Room { get; set; }

    public Guid AmenityId { get; set; }
    public Amenity? Amenity { get; set; }
}
