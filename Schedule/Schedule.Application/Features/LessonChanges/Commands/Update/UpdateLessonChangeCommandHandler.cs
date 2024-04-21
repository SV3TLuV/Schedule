using AutoMapper;
using MediatR;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.LessonChanges.Commands.Update;

public sealed class UpdateLessonChangeCommandHandler(
    ILessonChangeRepository lessonChangeRepository,
    IMapper mapper) : IRequestHandler<UpdateLessonChangeCommand, Unit>
{
    public async Task<Unit> Handle(UpdateLessonChangeCommand request, CancellationToken cancellationToken)
    {
        var lessonChange = mapper.Map<LessonChange>(request);
        await lessonChangeRepository.UpdateAsync(lessonChange, cancellationToken);
        return Unit.Value;
    }
}