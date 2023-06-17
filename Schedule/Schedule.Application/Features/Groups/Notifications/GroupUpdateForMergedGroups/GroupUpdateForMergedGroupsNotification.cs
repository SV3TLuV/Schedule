using MediatR;

namespace Schedule.Application.Features.Groups.Notifications.GroupUpdateForMergedGroups;

public sealed record GroupUpdateForMergedGroupsNotification(int Id) : INotification;