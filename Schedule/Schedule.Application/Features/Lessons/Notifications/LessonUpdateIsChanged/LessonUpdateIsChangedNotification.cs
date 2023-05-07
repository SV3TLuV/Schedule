using MediatR;

namespace Schedule.Application.Features.Lessons.Notifications.LessonUpdateIsChanged;

public sealed record LessonUpdateIsChangedNotification(int Id) : INotification;