using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class SpecialityViewModel : IMapWith<Speciality>
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public ICollection<DisciplineViewModel> Disciplines { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Speciality, SpecialityViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(speciality => speciality.SpecialityId))
            .ReverseMap();
    }
}