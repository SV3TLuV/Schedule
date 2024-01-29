using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Commands.Update;

public sealed class UpdateSpecialityCommand : IRequest<Unit>, IMapWith<Speciality>
{
    public required int Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required int MaxTermId { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Speciality, UpdateSpecialityCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(speciality => speciality.SpecialityId))
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(speciality =>
                    speciality.Name.Trim(' ', '.', ',').ToUpper()))
            .ForMember(command => command.Code, expression =>
                expression.MapFrom(speciality =>
                    speciality.Code.Trim(' ', '.', ',').ToUpper()));

        profile.CreateMap<UpdateSpecialityCommand, Speciality>()
            .ForMember(speciality => speciality.SpecialityId, expression =>
                expression.MapFrom(command => command.Id))
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(speciality =>
                    speciality.Name.Trim(' ', '.', ',').ToUpper()))
            .ForMember(command => command.Code, expression =>
                expression.MapFrom(speciality =>
                    speciality.Code.Trim(' ', '.', ',').ToUpper()));
    }
}