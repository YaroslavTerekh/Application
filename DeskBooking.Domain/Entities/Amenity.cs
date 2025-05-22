using DeskBooking.Domain.Entities.Base;
using DeskBooking.Domain.Enum.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.Entities;

public class Amenity : BaseEntity
{
    public required string AmenityName { get; set; }

    public AmenityType AmenityType { get; set; }
}
