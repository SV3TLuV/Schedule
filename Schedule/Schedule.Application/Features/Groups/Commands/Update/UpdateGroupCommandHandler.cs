using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Groups.Commands.Update;

public sealed class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateGroupCommandHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var groupDbo = await _context.Groups
            .FirstOrDefaultAsync(e => e.GroupId == request.Id, cancellationToken);
        
        if (groupDbo is null)
            throw new NotFoundException(nameof(Group), request.Id);
        
        var group = _mapper.Map<Group>(request);
        _context.Groups.Update(group);
        await _context.SaveChangesAsync(cancellationToken);
    }
}