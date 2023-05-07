using MediatR;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Notifications.LessonDeleteForUnitedGroups;

public sealed record LessonDeleteForUnitedGroupsNotification(Lesson Lesson) : INotification;