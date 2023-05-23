using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Disciplines.Commands.Update;

public class UpdateDisciplineCommandValidator : AbstractValidator<UpdateDisciplineCommand>
{
    public UpdateDisciplineCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
        RuleFor(query => query.Name)
            .MaximumLength(50)
            .NotNull();
        RuleFor(query => query.Code)
            .MaximumLength(20)
            .NotNull();
        RuleFor(query => query.TotalHours)
            .GreaterThan(0)
            .NotNull();
        RuleFor(query => query.TermId)
            .InclusiveBetween(1, 10)
            .NotNull();
        RuleFor(query => query.SpecialityId)
            .SetValidator(new IdValidator());
    }
}