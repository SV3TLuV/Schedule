using MediatR;

namespace Schedule.Application.Features.Groups.Commands.Unite;

public sealed class UniteGroupsCommand : IRequest
{
    public required int GroupId { get; set; }
    public required int GroupId2 { get; set; }
}