using FluentValidation;

namespace Schedule.Application.Features.DisciplineNames.Commands.Create;

public sealed class CreateDisciplineNameCommandValidator : AbstractValidator<CreateDisciplineNameCommand>
{
    public CreateDisciplineNameCommandValidator()
    {
        RuleFor(query => query.Name)
            .MaximumLength(50)
            .NotEmpty();
    }
}