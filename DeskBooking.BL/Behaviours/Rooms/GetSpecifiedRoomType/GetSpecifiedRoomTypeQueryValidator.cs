using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Rooms.GetSpecifiedRoomType;

public class GetSpecifiedRoomTypeQueryValidator : AbstractValidator<GetSpecifiedRoomTypeQuery>
{
    public GetSpecifiedRoomTypeQueryValidator()
    {
        RuleFor(x => x.RoomType)
            .IsInEnum().WithMessage(ValidationErrors.InvalidRoomType);
    }
}
