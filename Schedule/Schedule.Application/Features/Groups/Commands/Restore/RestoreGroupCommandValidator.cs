using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Groups.Commands.Restore;

public class RestoreGroupCommandValidator : AbstractValidator<RestoreGroupCommand>
{
    public RestoreGroupCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}