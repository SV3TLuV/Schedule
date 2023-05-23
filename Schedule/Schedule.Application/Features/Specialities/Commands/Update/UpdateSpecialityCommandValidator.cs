using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Specialities.Commands.Update;

public class UpdateSpecialityCommandValidator : AbstractValidator<UpdateSpecialityCommand>
{
    public UpdateSpecialityCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
        RuleFor(query => query.Code)
            .MaximumLength(20)
            .NotNull();
        RuleFor(query => query.Name)
            .MaximumLength(20)
            .NotNull();
        RuleFor(query => query.DisciplineIds)
            .NotEmpty();
    }
}