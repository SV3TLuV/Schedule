using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Reports.Queries.GetReportForDateRange;

public sealed class GetReportForDateRangeQueryValidator : AbstractValidator<GetReportForDateRangeQuery>
{
    public GetReportForDateRangeQueryValidator()
    {
        RuleFor(query => query.StartDateId)
            .SetValidator(new IdValidator());
        RuleFor(query => query.EndDateId)
            .SetValidator(new IdValidator());
    }
}