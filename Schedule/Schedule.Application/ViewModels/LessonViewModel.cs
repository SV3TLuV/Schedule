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
    
    public List<int> TeacherIds { get; set; } = null!;
    
    public List<int> ClassroomIds { get; set; } = null!;
    
    public LessonChangeViewModel? LessonChange { get; set; }

    public TimeOnly? TimeStart { get; set; }
    
    public TimeOnly? TimeEnd { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Lesson, LessonViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(lesson => lesson.LessonId))
            .ReverseMap();
    }
}