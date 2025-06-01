using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours;

public static class AddMediatrExtension
{
    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        return services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(AddMediatrExtension).Assembly));
    }
}