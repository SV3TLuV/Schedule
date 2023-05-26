using AutoMapper;
using MediatR;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Commands.Update;

public sealed class UpdateLessonTemplateCommand : IRequest, IMapWith<LessonTemplate>
{
    public required int Id { get; set; }
    public required int Number { get; set; }
    public required int? Subgroup { get; set; }
    public required int? TimeId { get; set; }
    public required int TemplateId { get; set; }
    public required int? DisciplineId { get; set; }
    public required ICollection<TeacherClassroomIdsViewModel> TeacherClassroomIds { get; set; }
    
    public void Map(Profile profile)
    {
        profile.CreateMap<LessonTemplate, UpdateLessonTemplateCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(lessonTemplate => lessonTemplate.LessonTemplateId));
        
        profile.CreateMap<UpdateLessonTemplateCommand, LessonTemplate>()
            .ForMember(lessonTemplate => lessonTemplate.LessonTemplateId, expression =>
                expression.MapFrom(command => command.Id));
    }
}