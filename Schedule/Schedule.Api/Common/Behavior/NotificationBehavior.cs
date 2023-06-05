using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Schedule.Api.Hubs;
using Schedule.Application.Common.Attributes;

namespace Schedule.Api.Common.Behavior;

public sealed class NotificationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationBehavior(
        IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestType = typeof(TRequest);
        var attribute = requestType.GetCustomAttribute(typeof(SignalRNotificationAttribute));

        if (attribute is SignalRNotificationAttribute notificationAttribute)
        {
            await _hubContext.Clients.All.SendAsync(
                "notified",
                notificationAttribute.ObjectType.Name,
                cancellationToken: cancellationToken);
        }
        
        return await next();
    }
}