using MediatR;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Notifications.LessonTemplateDeleteLessons;

public sealed record LessonTemplateDeleteLessonsNotification(LessonTemplate LessonTemplate) : INotification;