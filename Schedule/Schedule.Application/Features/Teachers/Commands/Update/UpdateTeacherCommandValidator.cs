using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Teachers.Commands.Update;

public class UpdateTeacherCommandValidator : AbstractValidator<UpdateTeacherCommand>
{
    public UpdateTeacherCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
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
    }
}