using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public sealed class TeacherClassroomIdPairViewModel : IMapWith<LessonTeacherClassroom>, IMapWith<LessonChangeTeacherClassroom>
{
    public int TeacherId { get; set; }
    public int ClassroomId { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<TeacherClassroomViewModel, LessonTeacherClassroom>()
            .ForMember(lessonTeacherClassroom => lessonTeacherClassroom.TeacherId, expression =>
                expression.MapFrom(viewModel => viewModel.Teacher.Id))
            .ForMember(lessonTeacherClassroom => lessonTeacherClassroom.ClassroomId, expression =>
                expression.MapFrom(viewModel => viewModel.Classroom.Id))
            .ReverseMap();

        profile.CreateMap<TeacherClassroomViewModel, LessonChangeTeacherClassroom>()
            .ForMember(lessonTeacherClassroom => lessonTeacherClassroom.TeacherId, expression =>
                expression.MapFrom(viewModel => viewModel.Teacher.Id))
            .ForMember(lessonTeacherClassroom => lessonTeacherClassroom.ClassroomId, expression =>
                expression.MapFrom(viewModel => viewModel.Classroom.Id))
            .ReverseMap();
    }
}