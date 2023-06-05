using MediatR;

namespace Schedule.Application.Features.Users.Notifications;

public sealed record UserSessionRevocationNotification(int UserId) : INotification;