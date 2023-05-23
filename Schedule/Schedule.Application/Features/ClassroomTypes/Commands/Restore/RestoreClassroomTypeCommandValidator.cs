using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.ClassroomTypes.Commands.Restore;

public class RestoreClassroomTypeCommandValidator : AbstractValidator<RestoreClassroomTypeCommand>
{
    public RestoreClassroomTypeCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}