using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Times.Queries.Get;

public sealed class GetTimeQueryValidator : AbstractValidator<GetTimeQuery>
{
    public GetTimeQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}