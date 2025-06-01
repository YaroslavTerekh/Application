using DeskBooking.Domain.Common.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.Common.DataModels;

public class ContextErrorResponse
{
    public int Code { get; set; } = StatusCodes.Status500InternalServerError;

    public string Message { get; set; } = ErrorMessages.Status500InternalServerError;
}
