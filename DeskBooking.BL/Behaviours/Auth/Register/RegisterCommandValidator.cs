using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Auth.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(t => t.Email)
            .EmailAddress().WithMessage(ValidationErrors.InvalidEmailFormat)
            .NotEmpty().WithMessage(ValidationErrors.EmptyEmail);

        RuleFor(t => t.Name)
            .NotEmpty().WithMessage(ValidationErrors.EmptyName)
            .MaximumLength(40).WithMessage(ValidationErrors.NameTooLong)
            .MinimumLength(5).WithMessage(ValidationErrors.NameTooShort);
    }
}
