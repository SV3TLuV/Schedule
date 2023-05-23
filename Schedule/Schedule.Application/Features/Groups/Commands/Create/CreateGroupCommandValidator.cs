using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Groups.Commands.Create;

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(query => query.Number)
            .MaximumLength(2)
            .NotNull();
        RuleFor(query => query.SpecialityId)
            .SetValidator(new IdValidator());
        RuleFor(query => query.CourseId)
            .InclusiveBetween(1, 10)
            .NotNull();
        RuleFor(query => query.EnrollmentYear)
            .NotNull();
    }
}