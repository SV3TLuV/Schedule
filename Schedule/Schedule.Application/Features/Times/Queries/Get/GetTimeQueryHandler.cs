using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Times.Queries.Get;

public sealed class GetTimeQueryHandler : IRequestHandler<GetTimeQuery, TimeViewModel>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetTimeQueryHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<TimeViewModel> Handle(GetTimeQuery request, CancellationToken cancellationToken)
    {
        var time = await _context.Times
            .Include(e => e.Type)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TimeId == request.Id, cancellationToken);

        if (time is null)
            throw new NotFoundException(nameof(Time), request.Id);

        return _mapper.Map<TimeViewModel>(time);
    }
}