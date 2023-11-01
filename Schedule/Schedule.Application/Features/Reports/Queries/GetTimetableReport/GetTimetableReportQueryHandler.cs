using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Lessons.Queries.GetLessonNumberList;
using Schedule.Application.Features.Timetables.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Reports.Queries.GetTimetableReport;

public sealed class GetTimetableReportQueryHandler : IRequestHandler<GetTimetableReportQuery, ReportViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;

    public GetTimetableReportQueryHandler(
        IScheduleDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }
    
    public async Task<ReportViewModel> Handle(GetTimetableReportQuery request,
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
        
        using var book = new XLWorkbook();

        foreach (var date in dates)
        {
            await GenerateForDateAsync(book, date.DateId, cancellationToken);
        }

        await using var memory = new MemoryStream();
        book.SaveAs(memory);
        
        var reportName = startDate.DateId == endDate.DateId
            ? $"Report-{startDate.Value}.xlsx"
            : $"Report-[{startDate.Value} - {endDate.Value}].xlsx";
        
        return new ReportViewModel
        {
            Content = memory.ToArray(),
            ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ReportName = reportName
        };
    }

    private async Task GenerateForDateAsync(IXLWorkbook book, int dateId,
        CancellationToken cancellationToken = default)
    {
        var getLessonNumberListQuery = new GetLessonNumberListQuery(dateId);
        var lessonNumbers = await _mediator.Send(getLessonNumberListQuery, cancellationToken);
        var maxLessonNumber = lessonNumbers.Max();
        
        var getTimetableListQuery = new GetTimetableListQuery
        {
            Page = 1,
            PageSize = 100,
            DateId = dateId,
        };
        var timetableData = await _mediator.Send(getTimetableListQuery, cancellationToken);

        if (!timetableData.Items.Any())
        {
            return;
        }

        var date = timetableData.Items.First().Date;
        
        book.AddWorksheet($"{date.Value}");
        
        var worksheet = book.Worksheets.Last();
        
        ApplyWorksheetStyles(worksheet);
        AddDay(worksheet, date.Day, maxLessonNumber);
        AddPairNumbers(worksheet, lessonNumbers);
        
        var column = 3;
        
        while (timetableData.PageNumber <= timetableData.TotalPages)
        {
            var timetables = timetableData.Items;

            foreach (var timetable in timetables)
            {
                var range = worksheet.Range(1, column, 12, column + 1);
                AddTimetable(worksheet, timetable, range);
                column += 2;
            }
            
            getTimetableListQuery.Page++;
            timetableData = await _mediator.Send(getTimetableListQuery, cancellationToken);
        }
    }
    
    private static void ApplyWorksheetStyles(IXLWorksheet worksheet)
    {
        worksheet.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        worksheet.Style.Border.OutsideBorder = XLBorderStyleValues.None;
        worksheet.Style.Alignment.WrapText = true;
        worksheet.Style.Font.FontSize = 16;
        worksheet.RowHeight = 65;
    }
    
    private static void AddDay(IXLWorksheet worksheet, DayViewModel day, int maxLessonNumber)
    {
        worksheet.Cell("A2").Value = day.Name;
        worksheet.Cell("A2").Style.Alignment.TextRotation = 90;
        worksheet.Range($"A2:A{maxLessonNumber * 3 + 1}").Merge();
    }

    private static void AddPairNumbers(IXLWorksheet worksheet, IEnumerable<int> lessonNumbers)
    {
        foreach (var number in lessonNumbers)
        {
            var start = number * 3;
            worksheet.Range($"B{start - 1}:B{start + 1}").Merge().Value = number;
        }
    }
    
    private static void AddTimetable(IXLWorksheet worksheet, TimetableViewModel timetable, IXLRangeBase range)
    {
        var firstCell = range.FirstCell();
        var belowCell = firstCell.CellBelow();

        foreach (var lesson in timetable.Lessons)
        {
            var startCell = belowCell;
            var rightCell = belowCell.CellRight();
            belowCell.Value = lesson.Discipline?.Name.Name ?? "";
            worksheet.Range(belowCell, rightCell).Merge();
            belowCell.WorksheetColumn().Width = 30;
            belowCell = belowCell.CellBelow();

            var teacherClassrooms = lesson.TeacherClassrooms.ToList();
            belowCell.Value = teacherClassrooms.ElementAtOrDefault(0)?.Teacher.ShortFio ?? "";
            belowCell.WorksheetColumn().Width = 20;
            belowCell.CellRight().Value = teacherClassrooms.ElementAtOrDefault(0)?.Classroom?.Cabinet ?? "";
            belowCell.CellRight().WorksheetColumn().Width = 10;
            belowCell = belowCell.CellBelow();

            belowCell.Value = teacherClassrooms.ElementAtOrDefault(1)?.Teacher.ShortFio ?? "";
            belowCell.WorksheetColumn().Width = 20;
            belowCell.CellRight().Value = teacherClassrooms.ElementAtOrDefault(1)?.Classroom?.Cabinet ?? "";
            belowCell.CellRight().WorksheetColumn().Width = 10;
            worksheet.Range(startCell, belowCell.CellRight()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            belowCell = belowCell.CellBelow();
        }

        worksheet.Range(firstCell, firstCell.CellRight()).Merge().Value = timetable.GroupNames;
        firstCell.WorksheetColumn().Width = 30;
        firstCell.Style.Font.FontSize = 26;
    }
}