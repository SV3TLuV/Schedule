using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.Timetables.Queries.GetList;

public sealed class GetTimetableListQueryValidator : AbstractValidator<GetTimetableListQuery>
{
    public GetTimetableListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}