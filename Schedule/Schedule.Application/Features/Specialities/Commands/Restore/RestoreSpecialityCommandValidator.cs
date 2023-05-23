using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Specialities.Commands.Restore;

public class RestoreSpecialityCommandValidator : AbstractValidator<RestoreSpecialityCommand>
{
    public RestoreSpecialityCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}