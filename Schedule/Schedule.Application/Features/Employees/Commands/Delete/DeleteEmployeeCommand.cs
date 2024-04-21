using MediatR;

namespace Schedule.Application.Features.Employees.Commands.Delete;

public sealed class DeleteEmployeeCommand : IRequest<Unit>
{
    public int Id { get; set; }
}