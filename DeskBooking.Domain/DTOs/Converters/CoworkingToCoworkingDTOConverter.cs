using AutoMapper;
using DeskBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.DTOs.Converters;

public class CoworkingToCoworkingDTOConverter : ITypeConverter<Coworking, CoworkingDTO>
{
    public CoworkingDTO Convert(Coworking source, CoworkingDTO destination, ResolutionContext context)
    {
        var rooms = source.Rooms;

        return new CoworkingDTO
        {
            Address = source.Address,
            Description = source.Description,
            Name = source.Name,
            Id = source.Id,
            Photos = source.Photos.Select(cp => new CoworkingPhotoDTO { Id = cp.Id, ImagePath = cp.ImagePath }).ToList(),
            AvailableRooms = source.Rooms
                .GroupBy(r => r.RoomType)
                .Select(r => new CoworkingRooms
                {
                    RoomsType = r.Key,
                    Quantity = r.Count()
                })
                .ToList()
        };
    }
}
