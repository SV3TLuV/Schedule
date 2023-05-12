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
        profile.CreateMap<LessonTeacherClassroom, TeacherClassroomViewModel>()
            .ReverseMap();
        profile.CreateMap<LessonTemplateTeacherClassroom, TeacherClassroomViewModel>()
            .ReverseMap();
    }
}