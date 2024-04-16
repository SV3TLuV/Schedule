using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Days.Queries.GetCurrent;

public sealed class GetCurrentDayQueryHandler(
    IScheduleDbContext context,
    IDateInfoService dateInfoService,
    IMapper mapper)
    : IRequestHandler<GetCurrentDayQuery, DayViewModel>
{
    public async Task<DayViewModel> Handle(GetCurrentDayQuery request,
        CancellationToken cancellationToken)
    {
        var currentDayId = dateInfoService.CurrentDayId;
        var day = await context.Days
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.DayId == currentDayId, cancellationToken);
        return mapper.Map<DayViewModel>(day);
    }
}