using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Specialities.Commands.Delete;

public class DeleteSpecialityCommandValidator : AbstractValidator<DeleteSpecialityCommand>
{
    public DeleteSpecialityCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}