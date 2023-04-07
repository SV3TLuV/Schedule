using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.ViewModels;

public class TimeViewModel : IMapWith<Time>
{
    public int Id { get; set; }

    public string Start { get; set; } = null!;

    public string End { get; set; } = null!;

    public int LessonNumber { get; set; }

    public TimeTypeViewModel Type { get; set; } = null!;
    
    public bool IsDeleted { get; set; }
    
    public void Map(Profile profile)
    {
        profile.CreateMap<Time, TimeViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(time => time.TimeId))
            .ForMember(viewModel => viewModel.Start, expression =>
                expression.MapFrom(time => time.Start.ToString(@"hh\:mm")))
            .ForMember(viewModel => viewModel.End, expression =>
                expression.MapFrom(time => time.End.ToString(@"hh\:mm")))
            .ReverseMap();
    }
}