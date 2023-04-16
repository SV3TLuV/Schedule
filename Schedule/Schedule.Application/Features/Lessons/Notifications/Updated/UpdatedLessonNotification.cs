using MediatR;

namespace Schedule.Application.Features.Lessons.Notifications.Updated;

public sealed record UpdatedLessonNotification(int Id) : INotification;