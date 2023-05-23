using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Times.Commands.Restore;

public class RestoreTimeCommandValidator : AbstractValidator<RestoreTimeCommand>
{
    public RestoreTimeCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}