using AutoMapper;
using MediatR;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Employees.Commands.Create;

public sealed class CreateEmployeeCommandHandler(
    IEmployeeRepository employeeRepository,
    IMapper mapper) : IRequestHandler<CreateEmployeeCommand, int>
{
    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = mapper.Map<Employee>(request);
        return await employeeRepository.CreateAsync(employee, cancellationToken);
    }
}