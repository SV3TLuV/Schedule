using MediatR;

namespace Schedule.Application.Features.Groups.Notifications.GroupCreatedCreateTemplates;

public sealed record GroupCreatedCreateTemplatesNotification(int Id) : INotification;