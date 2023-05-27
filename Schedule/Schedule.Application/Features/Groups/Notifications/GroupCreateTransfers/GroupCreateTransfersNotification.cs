using MediatR;

namespace Schedule.Application.Features.Groups.Notifications.GroupCreateTransfers;

public sealed record GroupCreateTransfersNotification(int Id) : INotification;