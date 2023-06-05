using MediatR;

namespace Schedule.Application.Features.Users.Notifications.UserSessionRevocation;

public sealed record UserSessionRevocationNotification(int UserId) : INotification;