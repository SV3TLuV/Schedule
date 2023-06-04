using AutoMapper;
using MediatR;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Commands.Update;

public sealed class UpdateLessonCommand : IRequest, IMapWith<Lesson>, IMapWith<LessonTemplate>
{
    public required int Id { get; set; }
    public required int Number { get; set; }
    public int? Subgroup { get; set; }
    public int? TimeId { get; set; }
    public required int TimetableId { get; set; }
    public int? DisciplineId { get; set; }

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
                        ClassroomId = ltc.ClassroomId
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
                        ClassroomId = ids.ClassroomId
                    })));

        profile.CreateMap<LessonTemplate, UpdateLessonCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(lesson => lesson.LessonTemplateId))
            .ForMember(command => command.TeacherClassroomIds, expression =>
                expression.MapFrom(lesson => lesson.LessonTemplateTeacherClassrooms
                    .Select(ltc => new TeacherClassroomIdsViewModel
                    {
                        TeacherId = ltc.TeacherId,
                        ClassroomId = ltc.ClassroomId
                    })));

        profile.CreateMap<UpdateLessonCommand, LessonTemplate>()
            .ForMember(lesson => lesson.LessonTemplateId, expression =>
                expression.MapFrom(command => command.Id))
            .ForMember(lesson => lesson.LessonTemplateTeacherClassrooms, expression =>
                expression.MapFrom(command => command.TeacherClassroomIds.Select(ids =>
                    new LessonTemplateTeacherClassroom
                    {
                        LessonTemplateId = command.Id,
                        TeacherId = ids.TeacherId,
                        ClassroomId = ids.ClassroomId
                    })));
    }
}