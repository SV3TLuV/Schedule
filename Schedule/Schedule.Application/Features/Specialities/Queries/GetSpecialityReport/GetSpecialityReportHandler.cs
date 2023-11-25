using ClosedXML.Excel;
using MediatR;
using Schedule.Application.Features.Specialities.Queries.GetList;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Features.Specialities.Queries.GetSpecialityReport;

public class GetSpecialityReportHandler : IRequestHandler<GetSpecialityReportQuery, ReportViewModel>
{
    private readonly IMediator _mediator;
    
    public GetSpecialityReportHandler(
        IMediator mediator
            )
    {
        _mediator = mediator;
    }
    
    public async Task<ReportViewModel> Handle(GetSpecialityReportQuery request, CancellationToken cancellationToken)
    {
        using var book = new XLWorkbook();
        
        await GenerateSpecialityReport(book, cancellationToken);
        
        await using var memory = new MemoryStream();
        book.SaveAs(memory);
        
        var reportName = "SpecialityReport.xlsx";
        
        return new ReportViewModel
        {
            Content = memory.ToArray(),
            ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ReportName = reportName
        };
    }
    
    private async Task GenerateSpecialityReport(IXLWorkbook book, CancellationToken cancellationToken = default)
    {
        var getSpecialityListQuery = new GetSpecialityListQuery
        {
            Filter = QueryFilter.All,
            Page = 1,
            PageSize = 100
        };
        var specialitiesData = await _mediator.Send(getSpecialityListQuery, cancellationToken);

        if (!specialitiesData.Items.Any())
        {
            return;
        }

        book.AddWorksheet("Специальности");
        
        var worksheet = book.Worksheets.Last();
        
        ApplyWorksheetStyles(worksheet);

        var row = 2;

        while (specialitiesData.PageNumber <= specialitiesData.TotalPages)
        {
            var specialities = specialitiesData.Items;

            foreach (var speciality in specialities)
            {
                var range = worksheet.Range(row, 0, row, 4);
                AddSpeciality(speciality, range);
                row++;
            }

            getSpecialityListQuery.Page++;
            specialitiesData = await _mediator.Send(getSpecialityListQuery, cancellationToken);
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
        worksheet.Cell("A1").Value = "Код";
        worksheet.Cell("B1").Value = "Название";
        worksheet.Cell("C1").Value = "Кол-во семестров";
    }

    private static void AddSpeciality(SpecialityViewModel speciality, IXLRangeBase range)
    {
        var firstCell = range.FirstCell();
        var nextCell = firstCell.CellRight();

        firstCell.Value = speciality.Code;
        nextCell.Value = speciality.Name;
        nextCell = nextCell.CellRight();
        nextCell.Value = speciality.MaxTermId;
    }
}