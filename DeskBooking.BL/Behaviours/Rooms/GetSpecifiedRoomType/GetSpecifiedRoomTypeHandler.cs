using AutoMapper;
using DeskBooking.Domain.DatabaseConnection;
using DeskBooking.Domain.DTOs.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Rooms.GetSpecifiedRoomType;

public class GetSpecifiedRoomTypeHandler : IRequestHandler<GetSpecifiedRoomTypeQuery, SortedRoomDTO>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GetSpecifiedRoomTypeHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SortedRoomDTO> Handle(GetSpecifiedRoomTypeQuery request, CancellationToken cancellationToken)
    {
        var groupedRooms = await _context.Rooms
            .Where(r => r.RoomType == request.RoomType)
            .Include(r => r.OpenspaceDesks)
            .Include(r => r.Amenities)
                .ThenInclude(ra => ra.Amenity)
            .GroupBy(r => r.RoomType)
            .ToListAsync(cancellationToken);

        //todo: validate query

        return groupedRooms
            .Select(group => _mapper.Map<SortedRoomDTO>(group.ToList()))
            .FirstOrDefault();
    }
}
