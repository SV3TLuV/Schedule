using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public sealed class DisciplineCodeViewModel : IMapWith<DisciplineCode>
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<DisciplineCode, DisciplineCodeViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(discipline => discipline.DisciplineCodeId))
            .ReverseMap();
    }
}