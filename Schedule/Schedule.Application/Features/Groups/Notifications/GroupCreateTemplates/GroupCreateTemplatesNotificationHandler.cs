using System.Data;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Notifications.GroupCreateTemplates;

public sealed class GroupCreateTemplatesNotificationHandler
    : INotificationHandler<GroupCreateTemplatesNotification>
{
    private readonly IScheduleDbContext _context;
    private readonly IDbConnection _connection;

    public GroupCreateTemplatesNotificationHandler(
        IScheduleDbContext context,
        IDbConnection connection)
    {
        _context = context;
        _connection = connection;
    }
    
    public async Task Handle(GroupCreateTemplatesNotification notification, CancellationToken cancellationToken)
    {
        var group = await _context.Set<Group>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.GroupId == notification.Id, cancellationToken);

        if (group is null)
            throw new NotFoundException(nameof(Group), notification.Id);
        
        var parameters = new { GroupId = notification.Id };
        
        const string script = """
            INSERT INTO Templates (DayId, WeekTypeId, GroupId, TermId)
            SELECT DayId, WeekTypeId, g.GroupId, t.TermId
            FROM Days, WeekTypes
            JOIN Groups g ON g.GroupId = @GroupId
            JOIN Terms t ON t.TermId <= g.TermId
        """;

        await _connection.ExecuteAsync(script, parameters);
    }
}