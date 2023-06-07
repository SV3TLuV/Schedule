using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.TimeTypes.Commands.Create;

public sealed class CreateTimeTypeCommandHandler : IRequestHandler<CreateTimeTypeCommand, int>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateTimeTypeCommandHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateTimeTypeCommand request, CancellationToken cancellationToken)
    {
        var searched = await _context.Set<TimeType>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.Name == request.Name, cancellationToken);

        if (searched is not null)
            throw new AlreadyExistsException($"Вид времени: {searched.Name}");
        
        var timeType = _mapper.Map<TimeType>(request);
        await _context.Set<TimeType>().AddAsync(timeType, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return timeType.TimeTypeId;
    }
}