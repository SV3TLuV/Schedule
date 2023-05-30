using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class TeacherClassroomViewModel : IMapWith<LessonTeacherClassroom>, IMapWith<LessonTemplateTeacherClassroom>
{
    public required TeacherViewModel Teacher { get; set; }
    
    public ClassroomViewModel? Classroom { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<TeacherClassroomViewModel, LessonTeacherClassroom>()
            .ForMember(lessonTeacherClassroom => lessonTeacherClassroom.TeacherId, expression =>
                expression.MapFrom(viewModel => viewModel.Teacher.Id))
            .ForMember(lessonTeacherClassroom => lessonTeacherClassroom.ClassroomId, expression =>
                expression.MapFrom(viewModel => viewModel.Classroom.Id))
            .ReverseMap();
        
        profile.CreateMap<TeacherClassroomViewModel, LessonTemplateTeacherClassroom>()
            .ForMember(lessonTeacherClassroom => lessonTeacherClassroom.TeacherId, expression =>
                expression.MapFrom(viewModel => viewModel.Teacher.Id))
            .ForMember(lessonTeacherClassroom => lessonTeacherClassroom.ClassroomId, expression =>
                expression.MapFrom(viewModel => viewModel.Classroom.Id))
            .ReverseMap();
    }
}