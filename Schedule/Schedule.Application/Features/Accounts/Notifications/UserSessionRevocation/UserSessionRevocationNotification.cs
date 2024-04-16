using MediatR;

namespace Schedule.Application.Features.Accounts.Notifications.UserSessionRevocation;

public sealed record UserSessionRevocationNotification(int AccountId) : INotification;