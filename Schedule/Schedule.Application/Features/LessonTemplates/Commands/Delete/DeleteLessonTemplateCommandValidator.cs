using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.LessonTemplates.Commands.Delete;

public class DeleteLessonTemplateCommandValidator : AbstractValidator<DeleteLessonTemplateCommand>
{
    public DeleteLessonTemplateCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}