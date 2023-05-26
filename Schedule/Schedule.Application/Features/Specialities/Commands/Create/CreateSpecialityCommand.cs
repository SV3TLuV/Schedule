using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Commands.Create;

public sealed class CreateSpecialityCommand : IRequest<int>, IMapWith<Speciality>
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required int MaxTermId { get; set; }
    
    public void Map(Profile profile)
    {
        profile.CreateMap<Speciality, CreateSpecialityCommand>()
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(speciality =>
                    speciality.Name.ToUpper()))
            .ForMember(command => command.Code, expression =>
                expression.MapFrom(speciality =>
                    speciality.Code.ToUpper()));
        
        profile.CreateMap<CreateSpecialityCommand, Speciality>()
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(speciality =>
                    speciality.Name.ToUpper()))
            .ForMember(command => command.Code, expression =>
                expression.MapFrom(speciality =>
                    speciality.Code.ToUpper()));
    }
}