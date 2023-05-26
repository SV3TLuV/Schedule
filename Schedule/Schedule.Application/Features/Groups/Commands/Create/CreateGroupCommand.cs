using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Create;

public sealed class CreateGroupCommand : IRequest<int>, IMapWith<Group>
{
    public required string Number { get; set; }
    public required int TermId { get; set; }
    public required int EnrollmentYear { get; set; }
    public required int SpecialityId { get; set; }
    public ICollection<int> MergedGroupIds { get; set; }
    
    public void Map(Profile profile)
    {
        profile.CreateMap<Group, CreateGroupCommand>()
            .ForMember(command => command.MergedGroupIds, expression =>
                expression.MapFrom(group => group.GroupGroups
                    .Select(groupGroup => groupGroup.GroupId2)));
        
        profile.CreateMap<CreateGroupCommand, Group>()
            .ForMember(group => group.GroupGroups, expression =>
                expression.MapFrom(command => command.MergedGroupIds
                    .Select(groupId => new GroupGroup
                    {
                        GroupId2 = groupId,
                    })));
    }
}