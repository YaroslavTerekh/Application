using DeskBooking.Domain.DatabaseConnection;
using DeskBooking.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Auth.Register;

//WARNING: This is a temporarily version of auth handlers, it was created only for demonstrating Booking feature

public class RegisterHandler : IRequestHandler<RegisterCommand>
{
    private readonly DataContext _context;

    public RegisterHandler(DataContext context)
    {
        _context = context;
    }

    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new AppUser
        {
            AnonUserId = request.AnonUserId,
            Name = request.Name,
            Email = request.Email
        };

        await _context.AppUsers.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
