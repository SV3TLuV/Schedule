using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Times.Commands.Update;

public sealed class UpdateTimeCommandHandler : IRequestHandler<UpdateTimeCommand, Unit>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateTimeCommandHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateTimeCommand request, CancellationToken cancellationToken)
    {
        var timeDbo = await _context.Set<Time>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TimeId == request.Id, cancellationToken);

        if (timeDbo is null)
            throw new NotFoundException(nameof(Time), request.Id);

        var time = _mapper.Map<Time>(request);
        
        var searched = await _context.Set<Time>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e =>
                e.Start == time.Start &&
                e.End == time.End &&
                e.TypeId == time.TypeId, cancellationToken);

        if (searched is not null)
            throw new AlreadyExistsException($"Время: {time.Start}-{time.End}");
        
        _context.Set<Time>().Update(time);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}