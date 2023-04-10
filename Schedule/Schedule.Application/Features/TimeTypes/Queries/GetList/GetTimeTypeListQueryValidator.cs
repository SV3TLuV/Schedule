using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.TimeTypes.Queries.GetList;

public sealed class GetTimeTypeListQueryValidator : AbstractValidator<GetTimeTypeListQuery>
{
    public GetTimeTypeListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}