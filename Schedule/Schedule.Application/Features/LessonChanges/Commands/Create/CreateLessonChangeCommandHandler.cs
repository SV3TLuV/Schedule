using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.LessonChanges.Commands.Create;

public sealed class CreateLessonChangeCommandHandler(
    ILessonChangeRepository lessonChangeRepository,
    IMapper mapper) : IRequestHandler<CreateLessonChangeCommand, int>
{
    public async Task<int> Handle(CreateLessonChangeCommand request, CancellationToken cancellationToken)
    {
        var lessonChange = mapper.Map<LessonChange>(request);
        return await lessonChangeRepository.CreateAsync(lessonChange, cancellationToken);
    }
}