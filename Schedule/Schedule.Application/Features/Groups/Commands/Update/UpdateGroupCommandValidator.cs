using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Groups.Commands.Update;

public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
{
    public UpdateGroupCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
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