using System.Globalization;
using AutoMapper;
using MediatR;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonChanges.Commands.Create;

public sealed class CreateLessonChangeCommand : IRequest<int>, IMapWith<LessonChange>
{
    public int Number { get; set; }
    public int LessonId { get; set; }
    public DateOnly Date { get; set; }
    public int? Subgroup { get; set; }
    public string TimeStart { get; set; } = null!;
    public string TimeEnd { get; set; } = null!;
    public int DisciplineId { get; set; }
    public ICollection<TeacherClassroomIdPairViewModel> TeacherClassroomIdPair { get; set; } =
        Array.Empty<TeacherClassroomIdPairViewModel>();

    public void Map(Profile profile)
    {
        profile.CreateMap<CreateLessonChangeCommand, LessonChange>()
            .ForMember(lessonChange => lessonChange.TimeStart, expression =>
                expression.MapFrom(command => TimeOnly.Parse(command.TimeStart, CultureInfo.InvariantCulture)))
            .ForMember(lessonChange => lessonChange.TimeEnd, expression =>
                expression.MapFrom(command => TimeOnly.Parse(command.TimeEnd, CultureInfo.InvariantCulture)))
            .ForMember(lessonChange => lessonChange.LessonChangeTeacherClassrooms, expression =>
                expression.MapFrom(command => command.TeacherClassroomIdPair.Select(e =>
                    new LessonChangeTeacherClassroom
                    {
                        TeacherId = e.TeacherId,
                        ClassroomId = e.ClassroomId
                    })));
    }
}