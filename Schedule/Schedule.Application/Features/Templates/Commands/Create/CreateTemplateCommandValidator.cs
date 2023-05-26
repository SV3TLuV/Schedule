using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Templates.Commands.Create;

public class CreateTemplateCommandValidator : AbstractValidator<CreateTemplateCommand>
{
    public CreateTemplateCommandValidator()
    {
        RuleFor(query => query.GroupId)
            .SetValidator(new IdValidator());
        RuleFor(query => query.TermId)
            .InclusiveBetween(1, 10);
        RuleFor(query => query.DayId)
            .InclusiveBetween(1, 7);
        RuleFor(query => query.WeekTypeId)
            .InclusiveBetween(1, 2);
    }
}