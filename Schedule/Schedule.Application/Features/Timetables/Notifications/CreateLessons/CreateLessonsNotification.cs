using MediatR;

namespace Schedule.Application.Features.Timetables.Notifications.CreateLessons;

public sealed record CreateLessonsNotification(int TimetableId) : INotification;