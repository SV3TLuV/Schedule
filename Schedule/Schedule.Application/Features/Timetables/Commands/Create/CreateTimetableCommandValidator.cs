using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Timetables.Commands.Create;

public class CreateTimetableCommandValidator : AbstractValidator<CreateTimetableCommand>
{
    public CreateTimetableCommandValidator()
    {
        RuleFor(query => query.DateId)
            .SetValidator(new IdValidator());
        RuleFor(query => query.GroupId)
            .SetValidator(new IdValidator());
    }
}