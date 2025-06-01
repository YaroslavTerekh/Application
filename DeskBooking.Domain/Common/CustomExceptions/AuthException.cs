using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.Common.CustomExceptions;

public class AuthException : Exception
{
    public int Code { get; set; }

    public AuthException(int code, string message) : base(message)
    {
        Code = code;
    }
}
