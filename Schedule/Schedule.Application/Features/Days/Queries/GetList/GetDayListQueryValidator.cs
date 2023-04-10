using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.Days.Queries.GetList;

public sealed class GetDayListQueryValidator : AbstractValidator<GetDayListQuery>
{
    public GetDayListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}