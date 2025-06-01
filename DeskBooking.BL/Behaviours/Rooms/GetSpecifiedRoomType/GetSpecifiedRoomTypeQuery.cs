using DeskBooking.Domain.DTOs.ResponseModels;
using DeskBooking.Domain.Enum.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Rooms.GetSpecifiedRoomType;

public record GetSpecifiedRoomTypeQuery(RoomType RoomType) : IRequest<SortedRoomDTO>;
