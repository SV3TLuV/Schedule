using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Times.Commands.Delete;

public class DeleteTimeCommandValidator : AbstractValidator<DeleteTimeCommand>
{
    public DeleteTimeCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}