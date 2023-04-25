using System.Data;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Dates.Notifications;

public sealed class DateCreateTimetablesNotificationHandler 
    : INotificationHandler<DateCreateTimetablesNotification>
{
    private readonly IScheduleDbContext _context;
    private readonly IDbConnection _connection;

    public DateCreateTimetablesNotificationHandler(
        IScheduleDbContext context,
        IDbConnection connection)
    {
        _context = context;
        _connection = connection;
    }
    
    public async Task Handle(DateCreateTimetablesNotification notification, CancellationToken cancellationToken)
    {
        var date = await _context.Set<Date>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DateId == notification.Id, cancellationToken);

        if (date is null)
            throw new NotFoundException(nameof(Date), cancellationToken);

        var parameters = new { DateId = notification.Id };
        
        const string script = """
            INSERT INTO Timetables (GroupId, DateId)
            SELECT GroupId, DateId
            FROM Groups, Dates
            WHERE Dates.DateId = @DateId;
        """;

        await _connection.ExecuteAsync(script, parameters);
    }
}