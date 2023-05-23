using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Teachers.Commands.Restore;

public class RestoreTeacherCommandValidator : AbstractValidator<RestoreTeacherCommand>
{
    public RestoreTeacherCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}