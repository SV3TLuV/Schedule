using FluentValidation;

namespace Schedule.Application.Features.Timetables.Queries.GetCurrentList;

public sealed class GetCurrentTimetableListQueryValidator : AbstractValidator<GetCurrentTimetableListQuery>
{
    public GetCurrentTimetableListQueryValidator()
    {
        RuleFor(query => query.GroupId)
            .Must(id => id is null or > 0);
        RuleFor(query => query.DateCount)
            .InclusiveBetween(1, 7);
    }
}