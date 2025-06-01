using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.Common.CustomExceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string description) : base(description) { }
}
