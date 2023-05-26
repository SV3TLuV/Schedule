using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.LessonTemplates.Commands.Create;

public class CreateLessonTemplateCommandValidator : AbstractValidator<CreateLessonTemplateCommand>
{
    public CreateLessonTemplateCommandValidator()
    {
        RuleFor(query => query.Number)
            .InclusiveBetween(1, 8);
        RuleFor(query => query.Subgroup)
            .GreaterThan(0);
        RuleFor(query => query.TimeId)
            .GreaterThan(0);
        RuleFor(query => query.TemplateId)
            .SetValidator(new IdValidator());
        RuleFor(query => query.DisciplineId)
            .GreaterThan(0);
    }
}