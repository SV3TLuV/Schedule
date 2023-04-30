using MediatR;

namespace Schedule.Application.Features.LessonTemplates.Notifications.CreatedOrUpdated;

public sealed record CreatedOrUpdatedLessonTemplateNotification(int Id) : INotification;