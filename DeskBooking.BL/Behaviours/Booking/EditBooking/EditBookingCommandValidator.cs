using DeskBooking.Domain.Enum.Rooms;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Booking.EditBooking;

public class EditBookingCommandValidator : AbstractValidator<EditBookingCommand>
{
    public EditBookingCommandValidator()
    {
        RuleFor(x => x.AnonUserId)
            .NotEmpty().WithMessage(ValidationErrors.EmptyAnonUserId);

        RuleFor(x => x.ReservationId)
            .NotEqual(Guid.Empty).WithMessage(ValidationErrors.EmptyReservationId);

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage(ValidationErrors.InvalidCapacity);

        RuleFor(x => x.RoomType)
            .IsInEnum().WithMessage(ValidationErrors.InvalidRoomType);

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate).WithMessage(ValidationErrors.InvalidDateRange);

        RuleFor(x => x)
            .Custom((command, context) =>
            {
                var duration = command.EndDate - command.StartDate;
                var max = command.RoomType switch
                {
                    RoomType.MeetingRoom => TimeSpan.FromDays(1),
                    RoomType.Openspace => TimeSpan.FromDays(30),
                    RoomType.PrivateRoom => TimeSpan.FromDays(30),
                    _ => TimeSpan.MaxValue
                };

                if (duration > max)
                {
                    context.AddFailure(string.Format(ValidationErrors.DurationLimitExceeded, max.TotalDays, command.RoomType));
                }
            });
    }
}