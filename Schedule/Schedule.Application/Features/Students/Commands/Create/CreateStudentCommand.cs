using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Students.Commands.Create;

public sealed class CreateStudentCommand : IRequest<int>, IMapWith<Student>
{

}