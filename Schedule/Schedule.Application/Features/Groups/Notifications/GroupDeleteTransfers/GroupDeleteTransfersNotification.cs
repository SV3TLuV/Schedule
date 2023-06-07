using MediatR;

namespace Schedule.Application.Features.Groups.Notifications.GroupDeleteTransfers;

public sealed record GroupDeleteTransfersNotification(int Id) : INotification;