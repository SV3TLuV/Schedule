using MediatR;

namespace Schedule.Application.Features.Lessons.Notifications.CreatedOrUpdated;

public sealed record CreatedOrUpdatedLessonNotification(int Id) : INotification;