using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Days.Queries.GetCurrent;

public sealed class GetCurrentDayQueryHandler : IRequestHandler<GetCurrentDayQuery, DayViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IDateInfoService _dateInfoService;
    private readonly IMapper _mapper;

    public GetCurrentDayQueryHandler(IScheduleDbContext context,
        IDateInfoService dateInfoService,
        IMapper mapper)
    {
        _context = context;
        _dateInfoService = dateInfoService;
        _mapper = mapper;
    }

    public async Task<DayViewModel> Handle(GetCurrentDayQuery request,
        CancellationToken cancellationToken)
    {
        var currentDayId = _dateInfoService.CurrentDayId;
        var day = await _context.Set<Day>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DayId == currentDayId, cancellationToken);
        return _mapper.Map<DayViewModel>(day);
    }
}