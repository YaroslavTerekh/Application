using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Auth.Register;

//WARNING: This is a temporarily version of auth handlers, it was created only for demonstrating Booking feature

public class RegisterCommand : IRequest
{
    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string AnonUserId { get; set; }
}
