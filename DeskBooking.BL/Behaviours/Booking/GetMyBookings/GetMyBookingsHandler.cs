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

namespace DeskBooking.BL.Behaviours.Booking.GetMyBookings;

public class GetMyBookingsHandler : IRequestHandler<GetMyBookingsQuery, List<BookingDTO>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GetMyBookingsHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<BookingDTO>> Handle(GetMyBookingsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Reservations
            .Include(r => r.AppUser)
            .Include(r => r.Room)
                .ThenInclude(r => r.Photos)
            .Where(r => r.AppUser.AnonUserId == request.anonUserId)
            .Select(r => _mapper.Map<BookingDTO>(r))
            .ToListAsync(cancellationToken);
    }
}
