using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Timetables.Queries.Get;

public sealed class GetTimetableQueryValidator : AbstractValidator<GetTimetableQuery>
{
    public GetTimetableQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}