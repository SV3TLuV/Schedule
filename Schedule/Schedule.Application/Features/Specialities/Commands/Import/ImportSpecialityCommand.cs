using MediatR;

namespace Schedule.Application.Features.Specialities.Commands.Import;

public sealed class ImportSpecialityCommand : IRequest<Unit>
{
    public required byte[] Content { get; set; }
}