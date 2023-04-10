using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.Dates.Queries.GetList;

public sealed class GetDateListQueryValidator : AbstractValidator<GetDateListQuery>
{
    public GetDateListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}