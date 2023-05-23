using FluentValidation;

namespace Schedule.Application.Features.TimeTypes.Commands.Create;

public class CreateTimeTypeCommandValidator : AbstractValidator<CreateTimeTypeCommand>
{
    public CreateTimeTypeCommandValidator()
    {
        RuleFor(query => query.Name)
            .MaximumLength(50)
            .NotNull();
    }
}