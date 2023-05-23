using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.TimeTypes.Commands.Restore;

public class RestoreTimeTypeCommandValidator : AbstractValidator<RestoreTimeTypeCommand>
{
    public RestoreTimeTypeCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}