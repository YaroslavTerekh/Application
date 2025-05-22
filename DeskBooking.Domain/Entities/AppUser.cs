using DeskBooking.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.Entities;

public class AppUser : BaseEntity
{
    public required string AnonUserId { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }
}
