using DeskBooking.Domain.Enum.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.DTOs.ResponseModels;

public class RoomTypesDTO
{
    public RoomType RoomType { get; set; }

    public List<RoomOptions> RoomOptions { get; set; } = [];
}

public class RoomOptions
{
    public int? Capacity { get; set; }

    public int? Quantity { get; set; }
}
