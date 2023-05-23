using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.TimeTypes.Commands.Delete;

public class DeleteTimeTypeCommandValidator : AbstractValidator<DeleteTimeTypeCommand>
{
    public DeleteTimeTypeCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}