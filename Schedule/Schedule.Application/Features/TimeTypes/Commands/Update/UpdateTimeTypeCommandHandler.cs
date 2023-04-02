using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.TimeTypes.Commands.Update;

public sealed class UpdateTimeTypeCommandHandler : IRequestHandler<UpdateTimeTypeCommand>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateTimeTypeCommandHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateTimeTypeCommand request, CancellationToken cancellationToken)
    {
        var timeTypeDbo = await _context.TimeTypes
            .FirstOrDefaultAsync(e => e.TimeTypeId == request.Id, cancellationToken);
        
        if (timeTypeDbo is null)
            throw new NotFoundException(nameof(TimeType), request.Id);
        
        var timeType = _mapper.Map<TimeType>(request);
        _context.TimeTypes.Update(timeType);
        await _context.SaveChangesAsync(cancellationToken);
    }
}