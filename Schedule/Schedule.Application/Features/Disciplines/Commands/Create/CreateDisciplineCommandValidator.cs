using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Disciplines.Commands.Create;

public class CreateDisciplineCommandValidator : AbstractValidator<CreateDisciplineCommand>
{
    public CreateDisciplineCommandValidator()
    {
        RuleFor(query => query.NameId)
            .GreaterThan(0);
        RuleFor(query => query.CodeId)
            .GreaterThan(0);
        RuleFor(query => query.TotalHours)
            .GreaterThan(0);
        RuleFor(query => query.TermId)
            .InclusiveBetween(1, 10);
        RuleFor(query => query.SpecialityId)
            .SetValidator(new IdValidator());
    }
}