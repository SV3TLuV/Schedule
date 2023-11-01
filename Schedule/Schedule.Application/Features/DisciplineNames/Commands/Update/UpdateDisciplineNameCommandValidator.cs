using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.DisciplineNames.Commands.Update;

public sealed class UpdateDisciplineNameCommandValidator : AbstractValidator<UpdateDisciplineNameCommand>
{
    public UpdateDisciplineNameCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
        RuleFor(query => query.Name)
            .MaximumLength(50)
            .NotEmpty();
    }
}