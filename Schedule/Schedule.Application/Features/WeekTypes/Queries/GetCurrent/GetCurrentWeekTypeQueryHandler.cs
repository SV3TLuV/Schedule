using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.WeekTypes.Queries.GetCurrent;

public sealed class GetCurrentWeekTypeQueryHandler(
    IScheduleDbContext context,
    IDateInfoService dateInfoService,
    IMapper mapper)
    : IRequestHandler<GetCurrentWeekTypeQuery, WeekTypeViewModel>
{
    public async Task<WeekTypeViewModel> Handle(GetCurrentWeekTypeQuery request,
        CancellationToken cancellationToken)
    {
        var currentWeekTypeId = (int)dateInfoService.CurrentWeekType;
        var weekType = await context.WeekTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.WeekTypeId == currentWeekTypeId, cancellationToken);
        return mapper.Map<WeekTypeViewModel>(weekType);
    }
}