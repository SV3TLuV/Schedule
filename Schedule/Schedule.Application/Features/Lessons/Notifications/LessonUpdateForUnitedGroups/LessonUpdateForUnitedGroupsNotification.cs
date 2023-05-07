using MediatR;

namespace Schedule.Application.Features.Lessons.Notifications.LessonUpdateForUnitedGroups;

public sealed record LessonUpdateForUnitedGroupsNotification(int LessonId) : INotification;