using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class TimetableViewModel : IMapWith<Timetable>
{
    public int Id { get; set; }

    public DateViewModel Date { get; set; } = null!;

    public ICollection<GroupViewModel> Groups { get; set; } = null!;

    public string GroupNames => string.Join(" ", Groups.Select(g => g.Name).Distinct());

    public ICollection<LessonViewModel> Lessons { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Timetable, TimetableViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(timetable => timetable.TimetableId))
            .ForMember(viewModel => viewModel.Groups, expression =>
                expression.MapFrom(timetable => new[] { timetable.Group }
                    .Concat(timetable.Group.GroupGroups
                        .Select(e => e.Group2))
                    .Concat(timetable.Group.GroupGroups1
                        .Select(e => e.Group))));

        profile.CreateMap<TimetableViewModel, Timetable>()
            .ForMember(timetable => timetable.TimetableId, expression =>
                expression.MapFrom(viewModel => viewModel.Id))
            .ForMember(timetable => timetable.Group, expression =>
                expression.MapFrom(viewModel => viewModel.Groups.First()));
    }
}