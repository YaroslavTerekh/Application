using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.Common.Settings;

public class GroqSettings
{
    public required string GroqAPI_KEY { get; set; }

    public required string GroqMODEL_NAME { get; set; }
}
