using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.ViewModels;

public class WeekTypeViewModel : IMapWith<WeekType>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<WeekType, WeekTypeViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(weekType => weekType.WeekTypeId))
            .ReverseMap();
    }
}