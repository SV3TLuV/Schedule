using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Terms.Queries.Get;

public sealed class GetTermQueryValidator : AbstractValidator<GetTermQuery>
{
    public GetTermQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}