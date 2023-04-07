using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.ViewModels;

public class LessonViewModel : IMapWith<Lesson>
{
    public int Id { get; set; }

    public int Number { get; set; }
    
    public int? Subgroup { get; set; }
    
    public int TimetableId { get; set; }

    public bool IsChanged { get; set; }

    public TimeViewModel Time { get; set; } = null!;
    
    public DisciplineViewModel Discipline { get; set; } = null!;

    public ICollection<LessonTeacherClassroomViewModel> TeacherClassrooms { get; set; } = null!;
    
    public void Map(Profile profile)
    {
        profile.CreateMap<Lesson, LessonViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(lesson => lesson.LessonId))
            .ReverseMap();
    }
}