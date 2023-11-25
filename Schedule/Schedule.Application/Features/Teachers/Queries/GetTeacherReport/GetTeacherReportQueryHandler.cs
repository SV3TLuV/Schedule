using ClosedXML.Excel;
using MediatR;
using Schedule.Application.Features.Teachers.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;

namespace Schedule.Application.Features.Teachers.Queries.GetTeacherReport;

public sealed class GetTeacherReportQueryHandler : IRequestHandler<GetTeacherReportQuery, ReportViewModel>
{
    private readonly IMediator _mediator;
    
    public GetTeacherReportQueryHandler(
        IMediator mediator
        )
    {
        _mediator = mediator;
    }
    
    public async Task<ReportViewModel> Handle(GetTeacherReportQuery request, CancellationToken cancellationToken)
    {
        using var book = new XLWorkbook();
        
        await GenerateTeacherReport(book, cancellationToken);
        
        await using var memory = new MemoryStream();
        book.SaveAs(memory);
        
        return new ReportViewModel
        {
            Content = memory.ToArray(),
            ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ReportName = "TeacherReport.xlsx"
        };
    }

    private async Task GenerateTeacherReport(IXLWorkbook book, CancellationToken cancellationToken = default)
    {
        var getTeacherListQuery = new GetTeacherListQuery
        {
            Filter = QueryFilter.All,
            Page = 1,
            PageSize = 100
        };
        var teachersData = await _mediator.Send(getTeacherListQuery, cancellationToken);

        if (!teachersData.Items.Any())
        {
            return;
        }

        book.AddWorksheet("Преподаватели");
        
        var worksheet = book.Worksheets.Last();
        
        ApplyWorksheetStyles(worksheet);

        var row = 2;

        while (teachersData.PageNumber <= teachersData.TotalPages)
        {
            var teachers = teachersData.Items;

            foreach (var teacher in teachers)
            {
                var range = worksheet.Range(row, 1, row, 4);
                AddTeacher(teacher, range);
                row++;
            }

            getTeacherListQuery.Page++;
            teachersData = await _mediator.Send(getTeacherListQuery, cancellationToken);
        }
    }
    
    private static void ApplyWorksheetStyles(IXLWorksheet worksheet)
    {
        worksheet.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        worksheet.Style.Border.OutsideBorder = XLBorderStyleValues.None;
        worksheet.Style.Alignment.WrapText = true;
        worksheet.Style.Font.FontSize = 16;
        worksheet.ColumnWidth = 130;
        worksheet.Cell("A1").Value = "Имя";
        worksheet.Cell("B1").Value = "Фамилия";
        worksheet.Cell("C1").Value = "Отчество";
        worksheet.Cell("D1").Value = "Почта";
    }

    private static void AddTeacher(TeacherViewModel teacher, IXLRangeBase range)
    {
        var firstCell = range.FirstCell();
        var nextCell = firstCell.CellRight();

        firstCell.Value = teacher.Name;
        nextCell.Value = teacher.Surname;
        nextCell = nextCell.CellRight();
        nextCell.Value = teacher.MiddleName;
        nextCell = nextCell.CellRight();
        nextCell.Value = teacher.Email;
    }
}