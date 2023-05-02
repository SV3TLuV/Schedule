using MediatR;

namespace Schedule.Application.Features.Timetables.Notifications.CreateLessons;

public sealed record TimetableCreateLessonsNotification(int TimetableId) : INotification;