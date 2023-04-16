using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public sealed class TeacherClassroomIdsViewModel : IMapWith<TeacherClassroomViewModel>
{
    public required int TeacherId { get; set; }
    
    public int? ClassroomId { get; set; }
    
    public void Map(Profile profile)
    {
        profile.CreateMap<TeacherClassroomIdsViewModel, TeacherClassroomViewModel>()
            .ForMember(destination => destination.Teacher, expression =>
                expression.MapFrom(ids => new Teacher
                {
                    TeacherId = ids.TeacherId
                }))
            .ForMember(destination => destination.Classroom, expression =>
                expression.MapFrom(ids => ids.ClassroomId.HasValue
                    ? new Classroom { ClassroomId = ids.ClassroomId.Value }
                    : null))
            .ReverseMap();
    }
}