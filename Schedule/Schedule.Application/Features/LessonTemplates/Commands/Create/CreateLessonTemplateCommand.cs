using AutoMapper;
using MediatR;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Commands.Create;

public sealed class CreateLessonTemplateCommand : IRequest<int>, IMapWith<LessonTemplate>
{
    public required int Number { get; set; }
    public required int? Subgroup { get; set; }
    public required int? TimeId { get; set; }
    public required int TimetableId { get; set; }
    public required int? DisciplineId { get; set; }
    public required ICollection<TeacherClassroomIdsViewModel> TeacherClassroomIds { get; set; } 
    
    public void Map(Profile profile)
    {
        profile.CreateMap<LessonTemplate, CreateLessonTemplateCommand>()
            .ForMember(command => command.TeacherClassroomIds, expression =>
                expression.MapFrom(lesson => lesson.LessonTemplateTeacherClassrooms
                    .Select(ltc => new TeacherClassroomIdsViewModel
                    {
                        TeacherId = ltc.TeacherId,
                        ClassroomId = ltc.ClassroomId,
                    })));
        
        profile.CreateMap<CreateLessonTemplateCommand, LessonTemplate>()
            .ForMember(lesson => lesson.LessonTemplateTeacherClassrooms, 
                expression =>
                expression.MapFrom(command => command.TeacherClassroomIds));
    }
}