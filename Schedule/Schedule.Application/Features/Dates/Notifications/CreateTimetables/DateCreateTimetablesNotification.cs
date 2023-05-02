using MediatR;

namespace Schedule.Application.Features.Dates.Notifications.CreateTimetables;

public sealed record DateCreateTimetablesNotification(int DateId) : INotification;