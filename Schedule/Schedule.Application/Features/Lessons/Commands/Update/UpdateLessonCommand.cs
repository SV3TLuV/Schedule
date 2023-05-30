using AutoMapper;
using MediatR;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Commands.Update;

public sealed class UpdateLessonCommand : IRequest, IMapWith<Lesson>
{
    public required int Id { get; set; }
    public required int Number { get; set; }
    public required int? Subgroup { get; set; }
    public required int? TimeId { get; set; }
    public required int TimetableId { get; set; }
    public required int? DisciplineId { get; set; }

    public ICollection<TeacherClassroomIdsViewModel>? TeacherClassroomIds { get; set; } =
        Array.Empty<TeacherClassroomIdsViewModel>();
    
    public void Map(Profile profile)
    {
        profile.CreateMap<Lesson, UpdateLessonCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(lesson => lesson.LessonId))
            .ForMember(command => command.TeacherClassroomIds, expression =>
                expression.MapFrom(lesson => lesson.LessonTeacherClassrooms
                    .Select(ltc => new TeacherClassroomIdsViewModel
                    {
                        TeacherId = ltc.TeacherId,
                        ClassroomId = ltc.ClassroomId,
                    })));
        
        profile.CreateMap<UpdateLessonCommand, Lesson>()
            .ForMember(lesson => lesson.LessonId, expression =>
                expression.MapFrom(command => command.Id))
            .ForMember(lesson => lesson.LessonTeacherClassrooms, expression =>
                expression.MapFrom(command => command.TeacherClassroomIds.Select(ids => 
                    new LessonTeacherClassroom
                    {
                        LessonId = command.Id,
                        TeacherId = ids.TeacherId,
                        ClassroomId = ids.ClassroomId,
                    })));
    }
}