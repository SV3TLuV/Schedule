using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Teachers.Commands.Import;

public sealed class ImportTeacherCommand : IRequest<Unit>
{
    public required byte[] Content { get; set; }
}