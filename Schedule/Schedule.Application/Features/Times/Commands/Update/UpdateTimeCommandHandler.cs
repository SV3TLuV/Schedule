using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Times.Commands.Update;

public sealed class UpdateTimeCommandHandler : IRequestHandler<UpdateTimeCommand>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateTimeCommandHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateTimeCommand request, CancellationToken cancellationToken)
    {
        var timeDbo = await _context.Times
            .FirstOrDefaultAsync(e => e.TimeId == request.Id, cancellationToken);
        
        if (timeDbo is null)
            throw new NotFoundException(nameof(Time), request.Id);
        
        var time = _mapper.Map<Time>(request);
        _context.Times.Update(time);
        await _context.SaveChangesAsync(cancellationToken);
    }
}