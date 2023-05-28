using MediatR;

namespace Schedule.Application.Features.Groups.Notifications.GroupCreateTimetables;

public sealed record GroupCreateTimetablesNotification(int Id) : INotification;