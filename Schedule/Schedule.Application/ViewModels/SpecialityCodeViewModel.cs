using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.ViewModels;

public class SpecialityCodeViewModel : IMapWith<SpecialityCode>
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;
    
    public void Map(Profile profile)
    {
        profile.CreateMap<SpecialityCode, SpecialityCodeViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(specialityCode => specialityCode.SpecialityCodeId))
            .ReverseMap();
    }
}