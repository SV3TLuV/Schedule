using System.Globalization;
using AutoMapper;
using MediatR;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Commands.Update;

public sealed class UpdateLessonCommand : IRequest<Unit>, IMapWith<Lesson>
{
    public int LessonId { get; set; }
    public int DisciplineId { get; set; }
    public int Number { get; set; }
    public int? Subgroup { get; set; }
    public string TimeStart { get; set; } = null!;
    public string TimeEnd { get; set; } = null!;
    public ICollection<TeacherClassroomIdPairViewModel> LessonTeacherClassrooms { get; set; } =
        Array.Empty<TeacherClassroomIdPairViewModel>();

    public void Map(Profile profile)
    {
        profile.CreateMap<UpdateLessonCommand, Lesson>()
            .ForMember(lesson => lesson.LessonId, expression =>
                expression.MapFrom(command => command.LessonId))
            .ForMember(lesson => lesson.TimeStart, expression =>
                expression.MapFrom(command => TimeOnly.Parse(command.TimeStart, CultureInfo.InvariantCulture)))
            .ForMember(lesson => lesson.TimeEnd, expression =>
                expression.MapFrom(command => TimeOnly.Parse(command.TimeEnd, CultureInfo.InvariantCulture)))
            .ForMember(lesson => lesson.LessonTeacherClassrooms, expression =>
                expression.MapFrom(command => command.LessonTeacherClassrooms.Select(e =>
                    new LessonTeacherClassroom
                    {
                        LessonId = command.LessonId,
                        TeacherId = e.TeacherId,
                        ClassroomId = e.ClassroomId
                    })));
    }
}