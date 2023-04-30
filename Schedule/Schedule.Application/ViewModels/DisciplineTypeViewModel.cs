using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class DisciplineTypeViewModel : IMapWith<DisciplineType>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public void Map(Profile profile)
    {
        profile.CreateMap<DisciplineType, DisciplineTypeViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(discipline => discipline.DisciplineTypeId))
            .ReverseMap();
    }
}