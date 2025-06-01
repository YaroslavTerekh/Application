using DeskBooking.Domain.DatabaseConnection;
using DeskBooking.Domain.DTOs.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Rooms.GetRoomTypes;

public class GetRoomTypesHandler : IRequestHandler<GetRoomTypesQuery, List<RoomTypesDTO>>
{
    private readonly DataContext _context;

    public GetRoomTypesHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<RoomTypesDTO>> Handle(GetRoomTypesQuery request, CancellationToken cancellationToken)
    {
        var roomTypes = await _context.Rooms
        .GroupBy(r => r.RoomType)
        .ToListAsync(cancellationToken);

        var result = roomTypes
            .Select(group => new RoomTypesDTO
            {
                RoomType = group.Key,
                RoomOptions = group.GroupBy(g => g.Capacity).Select(r => new RoomOptions
                {
                    Capacity = r.Key,
                    Quantity = r.Count()
                }).ToList()
            }).ToList();

        return result;
    }
}
