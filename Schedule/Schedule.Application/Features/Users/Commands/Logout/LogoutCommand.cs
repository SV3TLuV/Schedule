using MediatR;

namespace Schedule.Application.Features.Users.Commands.Logout;

public sealed class LogoutCommand : IRequest<Unit>
{
    public required string AccessToken { get; set; }
    
    public required string RefreshToken { get; set; }
}