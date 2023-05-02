using MediatR;

namespace Schedule.Application.Features.Templates.Notifications.CreateLessonTemplates;

public sealed record CreateLessonTemplatesNotification(int TemplateId) : INotification;