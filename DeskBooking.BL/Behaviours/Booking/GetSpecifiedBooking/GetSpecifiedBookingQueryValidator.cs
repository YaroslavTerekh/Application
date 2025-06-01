using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Booking.GetSpecifiedBooking;

public class GetSpecifiedBookingQueryValidator : AbstractValidator<GetSpecifiedBookingQuery>
{
    public GetSpecifiedBookingQueryValidator()
    {
        RuleFor(x => x.BookingId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrors.EmptyReservationId);
    }
}
