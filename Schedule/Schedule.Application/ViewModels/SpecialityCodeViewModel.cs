using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class SpecialityCodeViewModel : IMapWith<SpecialityCode>
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<SpecialityCode, SpecialityCodeViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(specialityCode => specialityCode.SpecialityCodeId))
            .ReverseMap();
    }
}