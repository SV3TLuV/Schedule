using MediatR;

namespace Schedule.Application.Features.Lessons.Notifications.Deleted;

public sealed record DeletedLessonNotification(int Id) : INotification;