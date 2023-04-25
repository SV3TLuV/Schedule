using MediatR;

namespace Schedule.Application.Features.Groups.Notifications.GroupCreateTemplates;

public sealed record GroupCreateTemplatesNotification(int Id) : INotification;