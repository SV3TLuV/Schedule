using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Teachers.Commands.Create;

public class CreateTeacherCommandValidator : AbstractValidator<CreateTeacherCommand>
{
    public CreateTeacherCommandValidator()
    {
        RuleFor(query => query.Name)
            .MaximumLength(40)
            .NotEmpty();
        RuleFor(query => query.Surname)
            .MaximumLength(40)
            .NotEmpty();
        RuleFor(query => query.MiddleName)
            .MaximumLength(40)
            .NotEmpty();
        RuleFor(query => query.Email)
            .MaximumLength(200)
            .NotEmpty();
        RuleFor(query => query.DisciplineIds)
            .SetValidator(new IdsValidator());
        RuleFor(query => query.GroupIds)
            .SetValidator(new IdsValidator());
    }
}