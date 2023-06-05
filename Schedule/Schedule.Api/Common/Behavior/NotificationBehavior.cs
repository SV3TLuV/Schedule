using MediatR;
using Microsoft.AspNetCore.SignalR;
using Schedule.Api.Hubs;

namespace Schedule.Api.Common.Behavior;

public sealed class NotificationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private const string Command = "Command";

    public NotificationBehavior(
        IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        if (!requestName.EndsWith(Command))
            return await next();

        var commandType = FirstWord(requestName);
        var objName = requestName
            .Replace(commandType, string.Empty)
            .Replace(Command, string.Empty);
        
        await _hubContext.Clients.All.SendAsync(
            "notified",
            objName,
            cancellationToken: cancellationToken);

        return await next();
    }
    
    private static string FirstWord(string line)
    {
        for (var i = 0; i < line.Length; i++)
            if (char.IsUpper(line[i]) && i != 0)
                return line[..i];
        return line;
    }
}