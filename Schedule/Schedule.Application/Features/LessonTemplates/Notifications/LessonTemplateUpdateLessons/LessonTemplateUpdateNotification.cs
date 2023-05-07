using MediatR;

namespace Schedule.Application.Features.LessonTemplates.Notifications.LessonTemplateUpdateLessons;

public sealed record LessonTemplateUpdateNotification(int LessonTemplateId) : INotification;