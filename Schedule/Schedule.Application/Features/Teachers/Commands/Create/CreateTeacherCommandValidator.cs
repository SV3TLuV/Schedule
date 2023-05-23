using FluentValidation;

namespace Schedule.Application.Features.Teachers.Commands.Create;

public class CreateTeacherCommandValidator : AbstractValidator<CreateTeacherCommand>
{
    public CreateTeacherCommandValidator()
    {
        RuleFor(query => query.Name)
            .MaximumLength(40)
            .NotNull();
        RuleFor(query => query.Surname)
            .MaximumLength(40)
            .NotNull();
        RuleFor(query => query.MiddleName)
            .MaximumLength(40)
            .NotNull();
        RuleFor(query => query.Email)
            .MaximumLength(200)
            .NotNull();
        /*RuleFor(query => query.DisciplineIds)
            .NotEmpty();
        RuleFor(query => query.GroupIds)
            .NotEmpty();*/
    }
}