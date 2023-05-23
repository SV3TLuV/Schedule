using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Disciplines.Commands.Delete;

public class DeleteDisciplineCommandValidator : AbstractValidator<DeleteDisciplineCommand>
{
    public DeleteDisciplineCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}