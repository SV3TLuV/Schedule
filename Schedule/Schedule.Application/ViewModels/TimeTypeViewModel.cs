using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.ViewModels;

public class TimeTypeViewModel : IMapWith<TimeType>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public void Map(Profile profile)
    {
        profile.CreateMap<TimeType, TimeTypeViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(timeType => timeType.TimeTypeId))
            .ReverseMap();
    }
}