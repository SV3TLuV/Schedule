using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public sealed class DisciplineNameViewModel : IMapWith<DisciplineName>
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public bool IsDeleted { get; set; }
    
    public void Map(Profile profile)
    {
        profile.CreateMap<DisciplineName, DisciplineNameViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(discipline => discipline.DisciplineNameId))
            .ReverseMap();
    }
}