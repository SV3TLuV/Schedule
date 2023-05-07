using MediatR;

namespace Schedule.Application.Features.Lessons.Notifications.LessonCreateForUnitedGroups;

public sealed record LessonCreateForUnitedGroupsNotification(int LessonId) : INotification;