using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Lessons.Commands.Create;

public class CreateLessonCommandValidator : AbstractValidator<CreateLessonCommand>
{
    public CreateLessonCommandValidator()
    {
        RuleFor(query => query.Number)
            .InclusiveBetween(1, 8);
        RuleFor(query => query.Subgroup)
            .GreaterThan(0);
        RuleFor(query => query.TimeId)
            .GreaterThan(0);
        RuleFor(query => query.TimetableId)
            .SetValidator(new IdValidator());
        RuleFor(query => query.DisciplineId)
            .GreaterThan(0);
    }
}