using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Classrooms.Commands.Restore;

public class RestoreClassroomCommandValidator : AbstractValidator<RestoreClassroomCommand>
{
    public RestoreClassroomCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}