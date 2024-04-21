using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Employees.Commands.Create;

public sealed class CreateEmployeeCommand : IRequest<int>, IMapWith<Employee>
{

}