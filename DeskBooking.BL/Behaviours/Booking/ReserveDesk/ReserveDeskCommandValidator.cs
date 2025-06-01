using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Booking.ReserveDesk;

public class ReserveDeskCommandValidator : AbstractValidator<ReserveDeskCommand>
{
    public ReserveDeskCommandValidator()
    {
        RuleFor(x => x.AnonUserId)
            .NotEmpty().WithMessage(ValidationErrors.EmptyAnonUserId);

        RuleFor(x => x.RoomId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrors.EmptyRoomId);

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate).WithMessage(ValidationErrors.InvalidDateRange);
    }
}