using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.WeekTypes.Queries.GetList;

public sealed class GetWeekTypeListQueryValidator : AbstractValidator<GetWeekTypeListQuery>
{
    public GetWeekTypeListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}