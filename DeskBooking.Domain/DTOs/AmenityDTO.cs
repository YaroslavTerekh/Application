using DeskBooking.Domain.Enum.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.DTOs;

public class AmenityDTO
{
    public Guid Id { get; set; }

    public required string AmenityName { get; set; }

    public AmenityType AmenityType { get; set; }
}
