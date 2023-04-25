using MediatR;

namespace Schedule.Application.Features.Timetables.Notifications.AddLessonsOnTimetableCreated;

public sealed record AddLessonsOnTimetableCreatedNotification(int Id) : INotification;