using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Employees.Commands.Update;

public sealed class UpdateEmployeeCommand : IRequest<Unit>, IMapWith<Employee>
{

}