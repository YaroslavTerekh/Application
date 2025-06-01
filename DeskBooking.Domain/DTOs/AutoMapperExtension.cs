using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.DTOs;

public static class AutoMapperExtension
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        return services.AddAutoMapper(typeof(AutoMapperExtension).Assembly);
    }
}
