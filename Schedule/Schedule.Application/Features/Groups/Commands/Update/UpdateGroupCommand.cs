using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Update;

public sealed class UpdateGroupCommand : IRequest<Unit>, IMapWith<Group>
{
    public required int Id { get; set; }
    public required string Number { get; set; }
    public required int EnrollmentYear { get; set; }
    public required int SpecialityId { get; set; }
    public bool IsAfterEleven { get; set; }
    public required int TermId { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Group, UpdateGroupCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(group => group.GroupId));

        profile.CreateMap<UpdateGroupCommand, Group>()
            .ForMember(group => group.GroupId, expression =>
                expression.MapFrom(command => command.Id));
    }
}