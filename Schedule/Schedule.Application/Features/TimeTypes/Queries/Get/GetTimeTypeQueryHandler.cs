using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.TimeTypes.Queries.Get;

public sealed class GetTimeTypeQueryHandler : IRequestHandler<GetTimeTypeQuery, TimeTypeViewModel>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetTimeTypeQueryHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<TimeTypeViewModel> Handle(GetTimeTypeQuery request, CancellationToken cancellationToken)
    {
        var timeType = await _context.TimeTypes
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TimeTypeId == request.Id, cancellationToken);

        if (timeType is null)
            throw new NotFoundException(nameof(TimeType), request.Id);

        return _mapper.Map<TimeTypeViewModel>(timeType);
    }
}