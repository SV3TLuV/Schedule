using MediatR;

namespace Schedule.Application.Features.LessonTemplates.Notifications.LessonTemplateCreateLessons;

public sealed record LessonTemplateCreateLessonsNotification(int LessonTemplateId) : INotification;