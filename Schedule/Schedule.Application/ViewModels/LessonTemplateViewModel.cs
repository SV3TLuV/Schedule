using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class LessonTemplateViewModel : IMapWith<LessonTemplate>, IMapWith<Lesson>, IMapWith<LessonViewModel>
{
    public int Id { get; set; }

    public int Number { get; set; }

    public int? Subgroup { get; set; }

    public int TemplateId { get; set; }

    public TimeViewModel Time { get; set; } = null!;

    public DisciplineViewModel Discipline { get; set; } = null!;

    public ICollection<LessonTemplateTeacherClassroom> TeacherClassrooms { get; set; } = null!;
    
    public void Map(Profile profile)
    {
        profile.CreateMap<LessonTemplate, LessonTemplateViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(lesson => lesson.LessonTemplateId))
            .ReverseMap();
        
        profile.CreateMap<LessonTemplate, Lesson>()
            .ForMember(viewModel => viewModel.LessonId, expression =>
                expression.MapFrom(lesson => lesson.LessonTemplateId))
            .ReverseMap();
        
        profile.CreateMap<LessonTemplate, LessonViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(lesson => lesson.LessonTemplateId))
            .ReverseMap();
    }
}