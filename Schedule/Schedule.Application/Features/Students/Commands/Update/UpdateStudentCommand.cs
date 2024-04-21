using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Students.Commands.Update;

public sealed class UpdateStudentCommand : IRequest<Unit>, IMapWith<Student>
{

}