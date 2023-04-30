using MediatR;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Notifications.Deleted;

public sealed record DeletedLessonTemplateNotification(LessonTemplate LessonTemplate) : INotification;