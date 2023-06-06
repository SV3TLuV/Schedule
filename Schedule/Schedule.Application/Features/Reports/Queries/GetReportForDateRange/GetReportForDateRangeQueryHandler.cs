using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Reports.Queries.GetReportForDate;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Reports.Queries.GetReportForDateRange;

public sealed class GetReportForDateRangeQueryHandler : IRequestHandler<GetReportForDateRangeQuery, ReportViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;

    public GetReportForDateRangeQueryHandler(
        IScheduleDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }
    
    public async Task<ReportViewModel> Handle(GetReportForDateRangeQuery request,
        CancellationToken cancellationToken)
    {
        var dates = await _context.Set<Date>()
            .AsNoTrackingWithIdentityResolution()
            .Where(e => e.DateId >= request.StartDateId && e.DateId <= request.EndDateId)
            .OrderBy(e => e.DateId)
            .ToListAsync(cancellationToken);

        var startDate = dates.FirstOrDefault();
        var endDate = dates.LastOrDefault();

        if (startDate is null)
        {
            throw new NotFoundException(nameof(Date), request.StartDateId);
        }
        
        if (endDate is null)
        {
            throw new NotFoundException(nameof(Date), request.EndDateId);
        }
        
        var reports = new List<ReportViewModel>();
        
        foreach (var date in dates)
        {
            var getReportForDateQuery = new GetReportForDateQuery(date.DateId);
            var report = await _mediator.Send(getReportForDateQuery, cancellationToken);
            reports.Add(report);
        }
        
        using var book = new XLWorkbook();

        foreach (var report in reports)
        {
            await using var memoryStream = new MemoryStream();
            await memoryStream.WriteAsync(report.Content, cancellationToken);
            using var tempBook = new XLWorkbook(memoryStream);
            var worksheet = tempBook.Worksheet(1);
            book.AddWorksheet(worksheet);
        }
        
        await using var memory = new MemoryStream();
        book.SaveAs(memory);

        return new ReportViewModel
        {
            Content = memory.ToArray(),
            ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ReportName = $"Report-[{startDate.Value} - {endDate.Value}].xlsx"
        };
    }
}