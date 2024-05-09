using System.Globalization;
using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class LessonViewModel : IMapWith<Lesson>
{
    public int Id { get; set; }
    
    public DisciplineViewModel? Discipline { get; set; }

    public int Number { get; set; }

    public int? Subgroup { get; set; }

    public int TimetableId { get; set; }
    
    public ICollection<LessonChangeViewModel>? LessonChanges { get; set; }

    public string? TimeStart { get; set; }

    public string? TimeEnd { get; set; }

    public ICollection<TeacherClassroomViewModel> LessonTeacherClassrooms { get; set; } =
        Array.Empty<TeacherClassroomViewModel>();

    public void Map(Profile profile)
    {
        profile.CreateMap<Lesson, LessonViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(lesson => lesson.LessonId))
            .ForMember(viewModel => viewModel.TimeStart, expression =>
                expression.MapFrom(lesson => lesson.TimeStart.ToString()))
            .ForMember(viewModel => viewModel.TimeEnd, expression =>
                expression.MapFrom(lesson => lesson.TimeEnd.ToString()))
            .ReverseMap();

        profile.CreateMap<LessonViewModel, Lesson>()
            .ForMember(lesson => lesson.LessonId, expression =>
                expression.MapFrom(viewModel => viewModel.Id))
            .ForMember(lesson => lesson.TimeStart, expression =>
                expression.MapFrom(viewModel => TimeOnly.Parse(viewModel.TimeStart ?? "00:00", CultureInfo.InvariantCulture)))
            .ForMember(lesson => lesson.TimeEnd, expression =>
                expression.MapFrom(viewModel => TimeOnly.Parse(viewModel.TimeEnd ?? "00:00", CultureInfo.InvariantCulture)))
            .ReverseMap();
    }
}