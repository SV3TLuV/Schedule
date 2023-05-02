using AutoMapper;
using MediatR;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Commands.Create;

public sealed class CreateLessonCommand : IRequest<int>, IMapWith<Lesson>
{
    public required int Number { get; set; }
    public required int TimetableId { get; set; }
    public int? Subgroup { get; set; }
    public int? TimeId { get; set; }
    public int? DisciplineId { get; set; }
    public ICollection<TeacherClassroomIdsViewModel>? TeacherClassroomIds { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Lesson, CreateLessonCommand>()
            .ForMember(command => command.TeacherClassroomIds, expression =>
                expression.MapFrom(lesson => lesson.LessonTeacherClassrooms
                    .Select(ltc => new TeacherClassroomIdsViewModel
                    {
                        TeacherId = ltc.TeacherId,
                        ClassroomId = ltc.ClassroomId,
                    })));
        
        profile.CreateMap<CreateLessonCommand, Lesson>()
            .ForMember(lesson => lesson.LessonTeacherClassrooms, expression =>
                expression.MapFrom(command => command.TeacherClassroomIds));
    }
}