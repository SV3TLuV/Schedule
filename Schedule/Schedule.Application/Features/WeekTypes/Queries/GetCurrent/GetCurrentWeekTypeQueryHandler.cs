using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.WeekTypes.Queries.GetCurrent;

public sealed class GetCurrentWeekTypeQueryHandler : IRequestHandler<GetCurrentWeekTypeQuery, WeekTypeViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IDateInfoService _dateInfoService;
    private readonly IMapper _mapper;

    public GetCurrentWeekTypeQueryHandler(IScheduleDbContext context,
        IDateInfoService dateInfoService,
        IMapper mapper)
    {
        _context = context;
        _dateInfoService = dateInfoService;
        _mapper = mapper;
    }

    public async Task<WeekTypeViewModel> Handle(GetCurrentWeekTypeQuery request,
        CancellationToken cancellationToken)
    {
        var currentWeekTypeId = (int)_dateInfoService.CurrentWeekType;
        var weekType = await _context.Set<WeekType>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.WeekTypeId == currentWeekTypeId, cancellationToken);
        return _mapper.Map<WeekTypeViewModel>(weekType);
    }
}