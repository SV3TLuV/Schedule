using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Reports.Queries.GetReportForDate;

public sealed class GetReportForDateQueryValidator : AbstractValidator<GetReportForDateQuery>
{
    public GetReportForDateQueryValidator()
    {
        RuleFor(query => query.DateId)
            .SetValidator(new IdValidator());
    }
}