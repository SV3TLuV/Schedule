using MediatR;

namespace Schedule.Application.Features.Specialities.Commands.Import;

public class ImportSpecialityCommand : IRequest<Unit>
{
    public required byte[] Content { get; set; }
    public required string ContentType { get; set; }
    public required string ReportName { get; set; }
}