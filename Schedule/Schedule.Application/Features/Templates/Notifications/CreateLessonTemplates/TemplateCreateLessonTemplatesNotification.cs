using MediatR;

namespace Schedule.Application.Features.Templates.Notifications.CreateLessonTemplates;

public sealed record TemplateCreateLessonTemplatesNotification(int TemplateId) : INotification;