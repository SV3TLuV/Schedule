using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Days.Queries.Get;

public sealed class GetDayQueryHandler : IRequestHandler<GetDayQuery, DayViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetDayQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DayViewModel> Handle(GetDayQuery request, CancellationToken cancellationToken)
    {
        var day = await _context.Set<Day>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DayId == request.Id, cancellationToken);

        if (day is null)
            throw new NotFoundException(nameof(Day), request.Id);

        return _mapper.Map<DayViewModel>(day);
    }
}