using AutoMapper;
using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Commands.Create;

[SignalRNotification(typeof(Lesson), CommandTypes.Create)]
public sealed class CreateLessonCommand : IRequest<int>, IMapWith<Lesson>, IMapWith<LessonTemplate>
{
    public required int Number { get; set; }
    public required int TimetableId { get; set; }
    public int? Subgroup { get; set; }
    public int? TimeId { get; set; }
    public int? DisciplineId { get; set; }

    public ICollection<TeacherClassroomIdsViewModel> TeacherClassroomIds { get; set; } =
        Array.Empty<TeacherClassroomIdsViewModel>();

    public void Map(Profile profile)
    {
        profile.CreateMap<Lesson, CreateLessonCommand>()
            .ForMember(command => command.TeacherClassroomIds, expression =>
                expression.MapFrom(lesson => lesson.LessonTeacherClassrooms
                    .Select(ltc => new TeacherClassroomIdsViewModel
                    {
                        TeacherId = ltc.TeacherId,
                        ClassroomId = ltc.ClassroomId
                    })));

        profile.CreateMap<CreateLessonCommand, Lesson>()
            .ForMember(lesson => lesson.LessonTeacherClassrooms, expression =>
                expression.MapFrom(command => command.TeacherClassroomIds.Select(ids =>
                    new LessonTeacherClassroom
                    {
                        TeacherId = ids.TeacherId,
                        ClassroomId = ids.ClassroomId
                    })));

        profile.CreateMap<LessonTemplate, CreateLessonCommand>()
            .ForMember(command => command.TeacherClassroomIds, expression =>
                expression.MapFrom(lesson => lesson.LessonTemplateTeacherClassrooms
                    .Select(ltc => new TeacherClassroomIdsViewModel
                    {
                        TeacherId = ltc.TeacherId,
                        ClassroomId = ltc.ClassroomId
                    })));

        profile.CreateMap<CreateLessonCommand, LessonTemplate>()
            .ForMember(lesson => lesson.LessonTemplateTeacherClassrooms, expression =>
                expression.MapFrom(command => command.TeacherClassroomIds.Select(ids =>
                    new LessonTemplateTeacherClassroom
                    {
                        TeacherId = ids.TeacherId,
                        ClassroomId = ids.ClassroomId
                    })));
    }
}