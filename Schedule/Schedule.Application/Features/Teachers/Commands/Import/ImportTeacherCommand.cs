using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Teachers.Commands.Import;

public class ImportTeacherCommand : IRequest<Unit>
{
    public required byte[] Content { get; set; }
    public required string ContentType { get; set; }
    public required string ReportName { get; set; }
}