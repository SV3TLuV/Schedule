using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.TimeTypes.Commands.Update;

public class UpdateTimeTypeCommandValidator : AbstractValidator<UpdateTimeTypeCommand>
{
    public UpdateTimeTypeCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
        RuleFor(query => query.Name)
            .MaximumLength(50)
            .NotNull();
    }
}