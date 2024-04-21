using AutoMapper;
using MediatR;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Students.Commands.Create;

public sealed class CreateStudentCommandHandler(
    IStudentRepository studentRepository,
    IMapper mapper) : IRequestHandler<CreateStudentCommand, int>
{
    public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = mapper.Map<Student>(request);
        return await studentRepository.CreateAsync(student, cancellationToken);
    }
}