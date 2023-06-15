using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.TimeTypes.Commands.Update;

public sealed class UpdateTimeTypeCommandHandler : IRequestHandler<UpdateTimeTypeCommand, Unit>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateTimeTypeCommandHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateTimeTypeCommand request, CancellationToken cancellationToken)
    {
        var timeTypeDbo = await _context.Set<TimeType>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TimeTypeId == request.Id, cancellationToken);

        if (timeTypeDbo is null)
            throw new NotFoundException(nameof(TimeType), request.Id);

        var timeType = _mapper.Map<TimeType>(request);
        _context.Set<TimeType>().Update(timeType);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}