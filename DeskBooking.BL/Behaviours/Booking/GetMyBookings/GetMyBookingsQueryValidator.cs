using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Booking.GetMyBookings;

public class GetMyBookingsQueryValidator : AbstractValidator<GetMyBookingsQuery>
{
    public GetMyBookingsQueryValidator()
    {
        RuleFor(x => x.anonUserId)
            .NotEmpty().WithMessage(ValidationErrors.EmptyAnonUserId);
    }
}
