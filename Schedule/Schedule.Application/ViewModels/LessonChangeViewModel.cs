using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class LessonChangeViewModel : IMapWith<LessonChange>, IMapWith<Lesson>, IMapWith<LessonViewModel>
{
    public int Id { get; set; }

    public int Number { get; set; }

    public int? Subgroup { get; set; }
    
    public TimeOnly? TimeStart { get; set; }
    
    public TimeOnly? TimeEnd { get; set; }
    
    public LessonViewModel Lesson { get; set; } = null!;

    public DisciplineViewModel Discipline { get; set; } = null!;
    
    public List<int> TeacherIds { get; set; } = null!;
    
    public List<int> ClassroomIds { get; set; } = null!;

    public DateOnly Date { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<LessonChange, LessonChangeViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(lesson => lesson.LessonChangeId))
            .ReverseMap();

        profile.CreateMap<LessonChange, Lesson>()
            .ForMember(viewModel => viewModel.LessonId, expression =>
                expression.MapFrom(lesson => lesson.LessonChangeId))
            .ReverseMap();

        profile.CreateMap<LessonChange, LessonViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(lesson => lesson.LessonChangeId))
            .ReverseMap();
    }
}