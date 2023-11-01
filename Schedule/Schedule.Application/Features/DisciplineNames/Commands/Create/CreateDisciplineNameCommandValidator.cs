using FluentValidation;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Create;

public sealed class CreateDisciplineNameCommandValidator : AbstractValidator<CreateDisciplineNameCommand>
{
    public CreateDisciplineNameCommandValidator()
    {
        RuleFor(query => query.Name)
            .MaximumLength(50)
            .NotEmpty();
    }
}