using FluentValidation;

namespace Schedule.Application.Features.Sessions.Commands.Delete;

public sealed class DeleteSessionCommandValidator : AbstractValidator<DeleteSessionCommand>
{
    public DeleteSessionCommandValidator()
    {
        RuleFor(query => query.Id)
            .NotEqual(Guid.Empty);
    }
}