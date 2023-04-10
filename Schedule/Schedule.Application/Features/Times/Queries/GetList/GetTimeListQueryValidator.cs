using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.Times.Queries.GetList;

public sealed class GetTimeListQueryValidator : AbstractValidator<GetTimeListQuery>
{
    public GetTimeListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}