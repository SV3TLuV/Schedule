using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Sessions.Commands.Update;

public sealed class UpdateSessionCommandHandler : IRequestHandler<UpdateSessionCommand>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateSessionCommandHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
    {
        var sessionDbo = await _context.Set<Session>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.SessionId == request.Id, cancellationToken);

        if (sessionDbo is null)
            throw new NotFoundException(nameof(Session), request.Id);

        var session = _mapper.Map<Session>(request);
        _context.Set<Session>().Update(session);
        await _context.SaveChangesAsync(cancellationToken);
    }
}