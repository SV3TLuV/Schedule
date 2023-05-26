using FluentValidation;

namespace Schedule.Application.Features.Specialities.Commands.Create;

public class CreateSpecialityCommandValidator : AbstractValidator<CreateSpecialityCommand>
{
    public CreateSpecialityCommandValidator()
    {
        RuleFor(query => query.Code)
            .MaximumLength(20)
            .NotEmpty();
        RuleFor(query => query.Name)
            .MaximumLength(20)
            .NotEmpty();
        RuleFor(query => query.DisciplineIds)
            .NotEmpty();
    }
}