using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Times.Commands.Update;

public sealed class UpdateTimeCommandHandler : IRequestHandler<UpdateTimeCommand>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateTimeCommandHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateTimeCommand request, CancellationToken cancellationToken)
    {
        var timeDbo = await _context.Set<Time>()
            .FirstOrDefaultAsync(e => e.TimeId == request.Id, cancellationToken);

        if (timeDbo is null)
            throw new NotFoundException(nameof(Time), request.Id);

        var time = _mapper.Map<Time>(request);
        _context.Set<Time>().Update(time);
        await _context.SaveChangesAsync(cancellationToken);
    }
}