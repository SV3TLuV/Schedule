using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Reports.Queries.GetTimetableReport;

public sealed class GetTimetableReportQueryValidator : AbstractValidator<GetTimetableReportQuery>
{
    public GetTimetableReportQueryValidator()
    {
        RuleFor(query => query.StartDateId)
            .SetValidator(new IdValidator());
        RuleFor(query => query.EndDateId)
            .SetValidator(new IdValidator());
    }
}