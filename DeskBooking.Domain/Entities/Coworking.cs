using DeskBooking.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.Entities;

public class Coworking : BaseEntity
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public required string Address { get; set; }

    public List<Room> Rooms { get; set; } = [];

    public List<CoworkingPhoto> Photos { get; set; } = [];
}
