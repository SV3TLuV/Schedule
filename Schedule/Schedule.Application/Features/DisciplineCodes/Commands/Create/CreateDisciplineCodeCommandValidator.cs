using FluentValidation;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Create;

public sealed class CreateDisciplineCodeCommandValidator : AbstractValidator<CreateDisciplineCodeCommand>
{
    public CreateDisciplineCodeCommandValidator()
    {
        RuleFor(query => query.Code)
            .MaximumLength(20)
            .NotEmpty();
    }
}