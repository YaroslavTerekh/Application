using AutoMapper;
using DeskBooking.Domain.DTOs.Converters;
using DeskBooking.Domain.DTOs.ResponseModels;
using DeskBooking.Domain.Entities;
using DeskBooking.Domain.Enum.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.DTOs;

class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Room, RoomDTO>();
        CreateMap<Amenity, AmenityDTO>();
        CreateMap<List<Room>, SortedRoomDTO>().ConvertUsing<RoomListToSortedRoomConverter>();
        CreateMap<Coworking, CoworkingDTO>().ConvertUsing<CoworkingToCoworkingDTOConverter>();
        CreateMap<Reservation, BookingDTO>();
        CreateMap<AppUser, AppUserDTO>();
        CreateMap<RoomPhoto, RoomPhotoDTO>();
    }
}
