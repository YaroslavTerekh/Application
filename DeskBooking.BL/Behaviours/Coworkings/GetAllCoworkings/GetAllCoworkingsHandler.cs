using AutoMapper;
using DeskBooking.Domain.DatabaseConnection;
using DeskBooking.Domain.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeskBooking.BL.Behaviours.Coworkings.GetAllCoworkings;

public class GetAllCoworkingsHandler : IRequestHandler<GetAllCoworkingsQuery, List<CoworkingDTO>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GetAllCoworkingsHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CoworkingDTO>> Handle(GetAllCoworkingsQuery request, CancellationToken cancellationToken)
    {
        var coworkings = await _context.Coworking
            .Include(c => c.Rooms)
            .Include(c => c.Photos)
            .Select(c => _mapper.Map<CoworkingDTO>(c))
            .ToListAsync(cancellationToken);

        return coworkings;
    }
}
