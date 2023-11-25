using ClosedXML.Excel;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Commands.Import;

public class ImportSpecialityCommandHandler : IRequestHandler<ImportSpecialityCommand, Unit>
{
    private readonly IScheduleDbContext _context;

    public ImportSpecialityCommandHandler(
        IScheduleDbContext context
        )
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(ImportSpecialityCommand request, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream(request.Content);
        using var book = new XLWorkbook(memoryStream);
        var sheet = book.Worksheets.Last();
        
        var teacherList = sheet.RowsUsed().Skip(1)
            .Select(row => new Speciality
            {
                Code = GetValueFromCell<string>(row.Cell(1)),
                Name = GetValueFromCell<string>(row.Cell(2)),
                MaxTermId = GetValueFromCell<int>(row.Cell(3))
            })
            .ToList();

        await _context.Set<Speciality>().AddRangeAsync(teacherList, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
    
    static T GetValueFromCell<T>(IXLCell cell)
    {
        try
        {
            if (typeof(T) == typeof(string))
                return (T)Convert.ChangeType(cell.Value.ToString(), typeof(T));
            else if (typeof(T) == typeof(int))
                return (T)Convert.ChangeType(cell.GetValue<int>(), typeof(T));

            return default(T);
        }
        catch
        {
            return default(T);
        }
    }
}