using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Commands.Create;

public sealed class CreateTeacherCommand : IRequest<int>, IMapWith<Teacher>
{

}