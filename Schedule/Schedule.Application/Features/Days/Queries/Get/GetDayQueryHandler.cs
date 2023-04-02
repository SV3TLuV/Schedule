using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Days.Queries.Get;

public sealed class GetDayQueryHandler : IRequestHandler<GetDayQuery, DayViewModel>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetDayQueryHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<DayViewModel> Handle(GetDayQuery request, CancellationToken cancellationToken)
    {
        var day = await _context.Days
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DayId == request.Id, cancellationToken);

        if (day is null)
            throw new NotFoundException(nameof(Day), request.Id);

        return _mapper.Map<DayViewModel>(day);
    }
}