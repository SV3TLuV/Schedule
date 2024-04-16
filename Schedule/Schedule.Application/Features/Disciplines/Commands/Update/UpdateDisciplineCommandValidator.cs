using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Disciplines.Commands.Update;

public class UpdateDisciplineCommandValidator : AbstractValidator<UpdateDisciplineCommand>
{
    public UpdateDisciplineCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
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
        RuleFor(command => command.DisciplineTypeId)
            .GreaterThan(0);
    }
}