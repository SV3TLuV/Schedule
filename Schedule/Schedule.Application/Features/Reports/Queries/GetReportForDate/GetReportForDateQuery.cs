using AutoMapper;
using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Lessons.Queries.GetLessonNumberList;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Reports.Queries.GetReportForDate;

public sealed record GetReportForDateQuery(int DateId) : IRequest<ReportViewModel>;

public sealed class GetReportForDateQueryHandler : IRequestHandler<GetReportForDateQuery, ReportViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetReportForDateQueryHandler(
        IScheduleDbContext context,
        IMediator mediator,
        IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<ReportViewModel> Handle(GetReportForDateQuery request,
        CancellationToken cancellationToken)
    {
        var getLessonNumberListQuery = new GetLessonNumberListQuery(request.DateId);
        var lessonNumbers = await _mediator.Send(getLessonNumberListQuery, cancellationToken);
        
        var query = _context.Set<Timetable>()
            .Include(e => e.Group)
            .ThenInclude(e => e.GroupGroups)
            .ThenInclude(e => e.Group2)
            .ThenInclude(e => e.Speciality)
            .Include(e => e.Group)
            .ThenInclude(e => e.GroupGroups)
            .ThenInclude(e => e.Group2)
            .ThenInclude(e => e.Term)
            .ThenInclude(e => e.Course)
            .Include(e => e.Group)
            .ThenInclude(e => e.GroupGroups1)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Speciality)
            .Include(e => e.Group)
            .ThenInclude(e => e.GroupGroups1)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Term)
            .ThenInclude(e => e.Course)
            .Include(e => e.Group)
            .ThenInclude(e => e.Speciality)
            .Include(e => e.Group)
            .ThenInclude(e => e.Term)
            .ThenInclude(e => e.Course)
            .Include(e => e.Date)
            .ThenInclude(e => e.Day)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.Discipline)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.Time)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.LessonTeacherClassrooms)
            .ThenInclude(e => e.Teacher)
            .Include(e => e.Lessons)
            .ThenInclude(e => e.LessonTeacherClassrooms)
            .ThenInclude(e => e.Classroom)
            .OrderBy(e => e.TimetableId)
            .Where(e => !e.Group.IsDeleted)
            .Where(e => e.DateId == request.DateId)
            .AsNoTrackingWithIdentityResolution()
            .AsSplitQuery();

        var timetables = await query
            .OrderBy(e => e.Group.TermId)
            .ThenBy(e => string.Concat(e.Group.Speciality.Name, "-", e.Group.Number))
            .ToListAsync(cancellationToken);
        var viewModels = _mapper.Map<List<TimetableViewModel>>(timetables);

        var viewModelIdsForRemove = new List<int>();
        var groupIds = new List<int>();

        foreach (var viewModel in viewModels)
        {
            var viewModelGroupIds = viewModel.Groups
                .Select(g => g.Id)
                .ToArray();

            if (viewModelGroupIds.Any(id => groupIds.Contains(id)))
                viewModelIdsForRemove.Add(viewModel.Id);
            else
                groupIds.AddRange(viewModelGroupIds);
        }

        var viewModelsResult = viewModels
            .Where(v => !viewModelIdsForRemove.Contains(v.Id))
            .ToArray();

        
        var date = viewModelsResult.FirstOrDefault()?.Date;
        var fileName = date is not null 
            ? $"schedule-{date.Value}.xlsx"
            : "schedule.xlsx";;
        
        using var book = new XLWorkbook();
        book.AddWorksheet("sheet");
        var ws = book.Worksheet(1);
        ApplyWorksheetStyles(ws);

        ws.Cell("A2").Value = date?.Day.Name;
        ws.Cell("A2").Style.Alignment.TextRotation = 90;
        ws.Range("A2:A11").Merge();
        ws.Range("B2:B3").Merge().Value = "1";
        ws.Range("B4:B5").Merge().Value = "2";
        ws.Range("B6:B7").Merge().Value = "3";
        ws.Range("B8:B9").Merge().Value = "4";
        ws.Range("B10:B11").Merge().Value = "5";

        var startColumn = 3;
        var endColumn = 4;

        foreach (var viewModel in viewModelsResult)
        {
            var nextRange = ws.Range(1, startColumn, 12, endColumn);

            GenerateGroupTimetable(viewModel, ws, nextRange);

            startColumn += 2;
            endColumn += 2;
        }
        
        await using var memory = new MemoryStream();
        book.SaveAs(memory);

        return new ReportViewModel
        {
            Content = memory.ToArray(),
            ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ReportName = fileName
        };
    }
    
    private static void GenerateGroupTimetable(TimetableViewModel timetable, IXLWorksheet ws, IXLRange range)
    {
        var firstCell = range.FirstCell();
        var belowCell = firstCell.CellBelow();

        foreach (var lesson in timetable.Lessons)
        {
            var startCell = belowCell;
            var rightCell = belowCell.CellRight();
            belowCell.Value = lesson.Discipline?.Name ?? "";
            ws.Range(belowCell, rightCell).Merge();
            SetColumnWidth(belowCell.WorksheetColumn(), 30);
            belowCell = belowCell.CellBelow();

            var teacherClassrooms = lesson.TeacherClassrooms.ToList();
            belowCell.Value = teacherClassrooms.ElementAtOrDefault(0)?.Teacher.Surname ?? "";
            SetColumnWidth(belowCell.WorksheetColumn(), 20);
            belowCell.CellRight().Value = teacherClassrooms.ElementAtOrDefault(0)?.Classroom?.Cabinet ?? "";
            SetColumnWidth(belowCell.CellRight().WorksheetColumn(), 10);
            belowCell = belowCell.CellBelow();

            belowCell.Value = teacherClassrooms.ElementAtOrDefault(1)?.Teacher.Surname ?? "";
            SetColumnWidth(belowCell.WorksheetColumn(), 20);
            belowCell.CellRight().Value = teacherClassrooms.ElementAtOrDefault(1)?.Classroom?.Cabinet ?? "";
            SetColumnWidth(belowCell.CellRight().WorksheetColumn(), 10);
            ws.Range(startCell, belowCell.CellRight()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            belowCell = belowCell.CellBelow();
        }

        ws.Range(firstCell, firstCell.CellRight()).Merge().Value = timetable.GroupNames;
        firstCell.WorksheetColumn().Width = 30;
        firstCell.Style.Font.FontSize = 26;
    }
        
    private static void ApplyWorksheetStyles(IXLWorksheet ws)
    {
        ws.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        ws.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        ws.Style.Font.FontSize = 16;
        ws.Style.Border.OutsideBorder = XLBorderStyleValues.None;
        ws.RowHeight = 65;
        ws.Style.Alignment.WrapText = true;
    }

    private static void SetColumnWidth(IXLColumn column, double width)
    {
        column.Width = width;
    }
}