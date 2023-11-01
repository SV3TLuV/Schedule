using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Delete;

public sealed class DeleteDisciplineCodeCommandValidator : AbstractValidator<DeleteDisciplineCodeCommand>
{
    public DeleteDisciplineCodeCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}