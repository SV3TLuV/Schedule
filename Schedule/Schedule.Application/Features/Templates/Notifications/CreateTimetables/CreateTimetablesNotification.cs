using MediatR;

namespace Schedule.Application.Features.Templates.Notifications.CreateTimetables;

public sealed record CreateTimetablesNotification(int TemplateId) : INotification;