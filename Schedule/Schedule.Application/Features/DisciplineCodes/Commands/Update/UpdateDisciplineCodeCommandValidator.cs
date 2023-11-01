using FluentValidation;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Update;

public sealed class UpdateDisciplineCodeCommandValidator : AbstractValidator<UpdateDisciplineCodeCommand>
{
    public UpdateDisciplineCodeCommandValidator()
    {
        RuleFor(query => query.Code)
            .MaximumLength(20)
            .NotEmpty();
    }
}