using AutoMapper;
using MediatR;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Commands.Update;

public sealed class UpdateLessonTemplateCommand : IRequest<Unit>, IMapWith<LessonTemplate>
{
    public required int Id { get; set; }
    public required int Number { get; set; }
    public required int? Subgroup { get; set; }
    public required int? TimeId { get; set; }
    public required int TemplateId { get; set; }
    public required int? DisciplineId { get; set; }

    public ICollection<TeacherClassroomIdsViewModel> TeacherClassroomIds { get; set; }
        = Array.Empty<TeacherClassroomIdsViewModel>();

    public void Map(Profile profile)
    {
        profile.CreateMap<LessonTemplate, UpdateLessonTemplateCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(lessonTemplate => lessonTemplate.LessonTemplateId))
            .ForMember(command => command.TeacherClassroomIds, expression =>
                expression.MapFrom(lesson => lesson.LessonTemplateTeacherClassrooms
                    .Select(ltc => new TeacherClassroomIdsViewModel
                    {
                        TeacherId = ltc.TeacherId,
                        ClassroomId = ltc.ClassroomId
                    })));

        profile.CreateMap<UpdateLessonTemplateCommand, LessonTemplate>()
            .ForMember(lessonTemplate => lessonTemplate.LessonTemplateId, expression =>
                expression.MapFrom(command => command.Id))
            .ForMember(lesson => lesson.LessonTemplateTeacherClassrooms,
                expression =>
                    expression.MapFrom(command => command.TeacherClassroomIds.Select(ids =>
                        new LessonTemplateTeacherClassroom
                        {
                            LessonTemplateId = command.Id,
                            TeacherId = ids.TeacherId,
                            ClassroomId = ids.ClassroomId
                        })));
    }
}