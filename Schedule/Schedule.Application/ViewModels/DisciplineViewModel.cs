using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class DisciplineViewModel : IMapWith<Discipline>
{
    public int Id { get; set; }

    public DisciplineNameViewModel Name { get; set; } = null!;

    public DisciplineCodeViewModel Code { get; set; } = null!;
    public DisciplineTypeViewModel Type { get; set; } = null!;

    public TermViewModel Term { get; set; } = null!;

    public SpecialityViewModel Speciality { get; set; } = null!;

    public int TotalHours { get; set; }

    public bool IsDeleted { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Discipline, DisciplineViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(discipline => discipline.DisciplineId))
            .ReverseMap();
    }
}