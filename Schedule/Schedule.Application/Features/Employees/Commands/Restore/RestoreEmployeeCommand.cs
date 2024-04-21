using MediatR;

namespace Schedule.Application.Features.Employees.Commands.Restore;

public sealed class RestoreEmployeeCommand : IRequest<Unit>
{
    public int Id { get; set; }
}