using MediatR;

namespace Schedule.Application.Features.Templates.Notifications.CreateTimetables;

public sealed record TemplateCreateTimetablesNotification(int TemplateId) : INotification;