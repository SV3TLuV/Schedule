using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Times.Commands.Create;

public class CreateTimeCommandValidator : AbstractValidator<CreateTimeCommand>
{
    public CreateTimeCommandValidator()
    {
        RuleFor(query => query.Start)
            .NotNull();
        RuleFor(query => query.End)
            .NotNull();
        RuleFor(query => query.LessonNumber)
            .InclusiveBetween(1, 10)
            .NotNull();
        RuleFor(query => query.TypeId)
            .SetValidator(new IdValidator());
    }
}