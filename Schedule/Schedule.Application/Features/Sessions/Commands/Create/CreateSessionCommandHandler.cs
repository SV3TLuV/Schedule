using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Sessions.Commands.Create;

public sealed class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, Guid>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateSessionCommandHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        var session = _mapper.Map<Session>(request);
        await _context.Set<Session>().AddAsync(session, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return session.SessionId;
    }
}