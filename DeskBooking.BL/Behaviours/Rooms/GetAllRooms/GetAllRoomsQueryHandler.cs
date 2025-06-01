using AutoMapper;
using DeskBooking.Domain.DatabaseConnection;
using DeskBooking.Domain.DTOs;
using DeskBooking.Domain.DTOs.ResponseModels;
using DeskBooking.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Rooms.GetAllRooms;

public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, List<SortedRoomDTO>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GetAllRoomsQueryHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<SortedRoomDTO>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        var groupedRooms = await _context.Rooms
            .Include(r => r.Photos)
            .Include(r => r.OpenspaceDesks)
            .Include(r => r.Amenities)
                .ThenInclude(ra => ra.Amenity)
            .GroupBy(r => r.RoomType)
            .ToListAsync(cancellationToken);

        return groupedRooms
            .Select(group => _mapper.Map<SortedRoomDTO>(group.ToList()))
            .ToList();
    }
}
