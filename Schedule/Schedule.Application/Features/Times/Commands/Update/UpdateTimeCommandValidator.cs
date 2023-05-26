using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Times.Commands.Update;

public class UpdateTimeCommandValidator : AbstractValidator<UpdateTimeCommand>
{
    public UpdateTimeCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
        RuleFor(query => query.Start);
        RuleFor(query => query.End);
        RuleFor(query => query.LessonNumber)
            .InclusiveBetween(1, 8);
        RuleFor(query => query.TypeId)
            .SetValidator(new IdValidator());
    }
}