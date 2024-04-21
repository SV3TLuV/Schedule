using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Commands.Update;

public sealed class UpdateTeacherCommand : IRequest<Unit>, IMapWith<Teacher>
{

}