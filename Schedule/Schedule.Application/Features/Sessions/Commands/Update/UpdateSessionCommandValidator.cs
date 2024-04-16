using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Sessions.Commands.Update;

public sealed class UpdateSessionCommandValidator : AbstractValidator<UpdateSessionCommand>
{
    public UpdateSessionCommandValidator()
    {
        RuleFor(query => query.Id)
            .NotEqual(Guid.Empty);
        RuleFor(query => query.RefreshToken)
            .MaximumLength(512);
        RuleFor(query => query.AccountId)
            .SetValidator(new IdValidator());
    }
}