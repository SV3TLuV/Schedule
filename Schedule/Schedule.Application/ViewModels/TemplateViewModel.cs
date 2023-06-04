using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class TemplateViewModel : IMapWith<Template>
{
    public int Id { get; set; }

    public DayViewModel Day { get; set; } = null!;

    public TermViewModel Term { get; set; } = null!;

    public ICollection<GroupViewModel> Groups { get; set; } = null!;

    public string GroupNames => string.Join(" ", Groups.Select(g => g.Name));

    public WeekTypeViewModel WeekType { get; set; } = null!;

    public ICollection<LessonTemplateViewModel> LessonTemplates { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Template, TemplateViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(timetable => timetable.TemplateId))
            .ForMember(viewModel => viewModel.Groups, expression =>
                expression.MapFrom(timetable => new[] { timetable.Group }
                    .Concat(timetable.Group.GroupGroups
                        .Select(e => e.Group2))
                    .Concat(timetable.Group.GroupGroups1
                        .Select(e => e.Group))));

        profile.CreateMap<TemplateViewModel, Template>()
            .ForMember(timetable => timetable.TemplateId, expression =>
                expression.MapFrom(viewModel => viewModel.Id))
            .ForMember(timetable => timetable.Group, expression =>
                expression.MapFrom(viewModel => viewModel.Groups.First()));
    }
}