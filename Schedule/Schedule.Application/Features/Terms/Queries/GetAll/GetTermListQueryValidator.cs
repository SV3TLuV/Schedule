using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.Terms.Queries.GetAll;

public sealed class GetTermListQueryValidator : AbstractValidator<GetTermListQuery>
{
    public GetTermListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}