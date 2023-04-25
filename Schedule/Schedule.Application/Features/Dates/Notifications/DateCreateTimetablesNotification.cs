using MediatR;

namespace Schedule.Application.Features.Dates.Notifications;

public sealed record DateCreateTimetablesNotification(int Id) : INotification;