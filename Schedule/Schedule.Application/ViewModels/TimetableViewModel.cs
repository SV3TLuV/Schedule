using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class TimetableViewModel : IMapWith<Timetable>
{
    public int Id { get; set; }

    public DateViewModel Date { get; set; } = null!;

    public GroupViewModel Group { get; set; } = null!;

    public ICollection<LessonViewModel> Lessons { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Timetable, TimetableViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(timetable => timetable.TimetableId))
            .ReverseMap();
    }
}