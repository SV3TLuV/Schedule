using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class DateViewModel : IMapWith<Date>
{
    public int Id { get; set; }

    public bool IsStudy { get; set; }

    public int Term { get; set; }

    public DateTime Value { get; set; }

    public DayViewModel Day { get; set; } = null!;

    public TimeTypeViewModel TimeType { get; set; } = null!;

    public WeekTypeViewModel WeekType { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Date, DateViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(date => date.DateId))
            .ReverseMap();
    }
}