using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.ViewModels;

public class DisciplineViewModel : IMapWith<Discipline>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public bool IsDeleted { get; set; }
    
    public void Map(Profile profile)
    {
        profile.CreateMap<Discipline, DisciplineViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(discipline => discipline.DisciplineId))
            .ReverseMap();
    }
}