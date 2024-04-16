using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Sessions.Commands.Create;

public sealed class CreateSessionCommandValidator : AbstractValidator<CreateSessionCommand>
{
    public CreateSessionCommandValidator()
    {
        RuleFor(query => query.RefreshToken)
            .MaximumLength(512);
        RuleFor(query => query.AccountId)
            .SetValidator(new IdValidator());
    }
}