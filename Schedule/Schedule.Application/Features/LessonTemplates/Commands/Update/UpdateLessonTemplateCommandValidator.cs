using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.LessonTemplates.Commands.Update;

public class UpdateLessonTemplateCommandValidator : AbstractValidator<UpdateLessonTemplateCommand>
{
    public UpdateLessonTemplateCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
        RuleFor(query => query.Number)
            .GreaterThan(0)
            .NotNull();
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