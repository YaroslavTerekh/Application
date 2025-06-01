using DeskBooking.Domain.DTOs;
using DeskBooking.Domain.DTOs.ResponseModels;
using DeskBooking.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Rooms.GetAllRooms;

public class GetAllRoomsQuery : IRequest<List<SortedRoomDTO>>
{

}
