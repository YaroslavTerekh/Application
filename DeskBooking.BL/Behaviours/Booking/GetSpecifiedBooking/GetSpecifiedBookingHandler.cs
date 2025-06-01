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

namespace DeskBooking.BL.Behaviours.Booking.GetSpecifiedBooking;

public class GetSpecifiedBookingHandler : IRequestHandler<GetSpecifiedBookingQuery, BookingDTO>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GetSpecifiedBookingHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BookingDTO> Handle(GetSpecifiedBookingQuery request, CancellationToken cancellationToken)
    { 
        var booking = await _context.Reservations
            .Include(r => r.AppUser)
            .Include(r => r.Room)
            .Where(r => r.Id == request.BookingId)
            .Select(r => _mapper.Map<BookingDTO>(r))
            .FirstOrDefaultAsync(cancellationToken);

        if(booking is null)
        {
            throw new NotFoundException(ErrorMessages.BookingNotFound);
        }

        return booking;
    }
}
