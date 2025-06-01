using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.Common.CustomExceptions;

public class RequestException : Exception
{
    public int Code { get; set; }

    public RequestException(int code, string description) : base(description)
    {
        Code = code;    
    }
}
