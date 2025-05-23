using AutoMapper;
using DeskBooking.Domain.DTOs.ResponseModels;
using DeskBooking.Domain.Entities;
using DeskBooking.Domain.Enum.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.DTOs.Converters;

public class RoomListToSortedRoomConverter : ITypeConverter<List<Room>, SortedRoomDTO>
{
    public SortedRoomDTO Convert(List<Room> rooms, SortedRoomDTO destination, ResolutionContext context)
    {
        var firstRoom = rooms.FirstOrDefault();

        return new SortedRoomDTO
        {
            RoomName = firstRoom != null ? firstRoom.RoomName : "Unknown",
            Description = firstRoom != null ? firstRoom.Description : "",
            RoomType = firstRoom != null ? firstRoom.RoomType : RoomType.Openspace,
            CapacityOptions = string.Join(", ",
                rooms
                    .Select(r => r.Capacity)
                    .Where(c => c.HasValue)
                    .Select(c => c.Value.ToString())
                    .Distinct()
            ),
            Amenities = firstRoom.Amenities
                .Select(a => new AmenityDTO
                {
                    AmenityName = a.Amenity.AmenityName,
                    AmenityType = a.Amenity.AmenityType,
                    Id = a.AmenityId
                }).ToList(),
            Avaliabilities = rooms
                .GroupBy(r => r.Capacity)
                .Select(group => new Avaliability
                {
                    Capacity = group.Key,
                    Quantity = group.Count(),
                    Desks = group
                        .SelectMany(r => r.OpenspaceDesks)
                        .Select(desk => new OpenspaceDeskDTO
                        {
                            Id = desk.Id,
                            DeskNumber = desk.DeskNumber,
                            Room = null
                        })
                        .ToList()
                })
                .ToList()
        };
    }
}

