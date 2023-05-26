using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Disciplines.Commands.Create;

public class CreateDisciplineCommandValidator : AbstractValidator<CreateDisciplineCommand>
{
    public CreateDisciplineCommandValidator()
    {
        RuleFor(query => query.Name)
            .MaximumLength(50)
            .NotEmpty();
        RuleFor(query => query.Code)
            .MaximumLength(20)
            .NotEmpty();
        RuleFor(query => query.TotalHours)
            .GreaterThan(0);
        RuleFor(query => query.TermId)
            .InclusiveBetween(1, 10);
        RuleFor(query => query.SpecialityId)
            .SetValidator(new IdValidator());
    }
}