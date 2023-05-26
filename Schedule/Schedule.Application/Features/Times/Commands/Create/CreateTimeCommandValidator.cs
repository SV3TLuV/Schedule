using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Times.Commands.Create;

public class CreateTimeCommandValidator : AbstractValidator<CreateTimeCommand>
{
    public CreateTimeCommandValidator()
    {
        RuleFor(query => query.Start);
        RuleFor(query => query.End);
        RuleFor(query => query.LessonNumber)
            .InclusiveBetween(1, 8);
        RuleFor(query => query.TypeId)
            .SetValidator(new IdValidator());
    }
}