using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Sessions.Commands.Create;

public sealed class CreateSessionCommandHandler(
    IScheduleDbContext context,
    IMapper mapper,
    IDateInfoService dateInfoService) : IRequestHandler<CreateSessionCommand, Guid>
{
    public async Task<Guid> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        var session = mapper.Map<Session>(request);
        session.Created = dateInfoService.CurrentDate;

        await context.Sessions.AddAsync(session, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return session.SessionId;
    }
}