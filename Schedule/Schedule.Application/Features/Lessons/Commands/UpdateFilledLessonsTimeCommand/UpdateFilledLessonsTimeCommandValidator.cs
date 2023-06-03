using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Lessons.Commands.UpdateFilledLessonsTimeCommand;

public sealed class UpdateFilledLessonsTimeCommandValidator : AbstractValidator<UpdateFilledLessonsTimeCommand>
{
    public UpdateFilledLessonsTimeCommandValidator()
    {
        RuleFor(command => command.DateId)
            .SetValidator(new IdValidator());
        RuleFor(command => command.TimeTypeId)
            .SetValidator(new IdValidator());
    }
}