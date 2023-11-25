using ClosedXML.Excel;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Commands.Import;

public class ImportTeacherCommandHandler : IRequestHandler<ImportTeacherCommand, Unit>
{
    private readonly IScheduleDbContext _context;

    public ImportTeacherCommandHandler(
        IScheduleDbContext context
        )
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(ImportTeacherCommand request, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream(request.Content);
        using var book = new XLWorkbook(memoryStream);
        var sheet = book.Worksheets.Last();
        
        var teacherList = sheet.RowsUsed().Skip(1)
            .Select(row => new Teacher
            {
                Name = GetValueFromCell<string>(row.Cell(1)),
                Surname = GetValueFromCell<string>(row.Cell(2)),
                MiddleName = GetValueFromCell<string>(row.Cell(3)),
                Email = GetValueFromCell<string>(row.Cell(4))
            })
            .ToList();

        await _context.Set<Teacher>().AddRangeAsync(teacherList, cancellationToken);
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