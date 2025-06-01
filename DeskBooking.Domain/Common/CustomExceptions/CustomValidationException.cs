using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.Common.CustomExceptions;

public class CustomValidationException : Exception
{
    public CustomValidationException(IEnumerable<ValidationFailure> failures)
        : base(GenerateMessage(failures))
    {
    }

    private static string GenerateMessage(IEnumerable<ValidationFailure> failures)
    {
        return string.Join(" | ", failures.Select(f => $"{f.PropertyName}: {f.ErrorMessage}"));
    }
}