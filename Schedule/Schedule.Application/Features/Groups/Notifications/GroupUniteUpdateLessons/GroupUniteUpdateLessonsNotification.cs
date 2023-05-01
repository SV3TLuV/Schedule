using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Notifications.GroupUniteUpdateLessons;

public sealed record GroupUniteUpdateLessonsNotification(int GroupId, int GroupId2)
    : INotification;

public sealed class GroupUniteUpdateLessonsNotificationHandler
    : INotificationHandler<GroupUniteUpdateLessonsNotification>
{
    private readonly IScheduleDbContext _context;
    private readonly IDateInfoService _dateInfoService;
    private readonly IMediator _mediator;

    public GroupUniteUpdateLessonsNotificationHandler(
        IScheduleDbContext context,
        IDateInfoService dateInfoService,
        IMediator mediator)
    {
        _context = context;
        _dateInfoService = dateInfoService;
        _mediator = mediator;
    }
    
    public async Task Handle(GroupUniteUpdateLessonsNotification notification,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}