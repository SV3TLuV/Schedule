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
            .MinimumLength(2)
            .MaximumLength(2)
            .NotEmpty();
        RuleFor(query => query.SpecialityId)
            .SetValidator(new IdValidator());
        RuleFor(query => query.TermId)
            .InclusiveBetween(1, 10);
        RuleFor(query => query.EnrollmentYear);
    }
}