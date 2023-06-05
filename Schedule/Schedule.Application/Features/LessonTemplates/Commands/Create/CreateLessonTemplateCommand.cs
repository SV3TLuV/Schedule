using AutoMapper;
using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Commands.Create;

[SignalRNotification(typeof(LessonTemplate), CommandTypes.Create)]
public sealed class CreateLessonTemplateCommand : IRequest<int>, IMapWith<LessonTemplate>
{
    public required int Number { get; set; }
    public required int TemplateId { get; set; }
    public int? Subgroup { get; set; }
    public int? TimeId { get; set; }
    public int? DisciplineId { get; set; }

    public ICollection<TeacherClassroomIdsViewModel> TeacherClassroomIds { get; set; }
        = Array.Empty<TeacherClassroomIdsViewModel>();

    public void Map(Profile profile)
    {
        profile.CreateMap<LessonTemplate, CreateLessonTemplateCommand>()
            .ForMember(command => command.TeacherClassroomIds, expression =>
                expression.MapFrom(lesson => lesson.LessonTemplateTeacherClassrooms
                    .Select(ltc => new TeacherClassroomIdsViewModel
                    {
                        TeacherId = ltc.TeacherId,
                        ClassroomId = ltc.ClassroomId
                    })));

        profile.CreateMap<CreateLessonTemplateCommand, LessonTemplate>()
            .ForMember(lesson => lesson.LessonTemplateTeacherClassrooms,
                expression =>
                    expression.MapFrom(command => command.TeacherClassroomIds.Select(ids =>
                        new LessonTemplateTeacherClassroom
                        {
                            TeacherId = ids.TeacherId,
                            ClassroomId = ids.ClassroomId
                        })));
    }
}