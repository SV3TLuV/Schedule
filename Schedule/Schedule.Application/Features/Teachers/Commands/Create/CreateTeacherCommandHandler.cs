using AutoMapper;
using MediatR;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Teachers.Commands.Create;

public sealed class CreateTeacherCommandHandler(
    ITeacherRepository teacherRepository,
    IMapper mapper) : IRequestHandler<CreateTeacherCommand, int>
{
    public async Task<int> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = mapper.Map<Teacher>(request);
        return await teacherRepository.CreateAsync(teacher, cancellationToken);
    }
}