using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Commands.Update;

public sealed class UpdateLessonCommand : IRequest, IMapWith<Lesson>
{
    public required int Id { get; set; } 
}