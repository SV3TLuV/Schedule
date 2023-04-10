using Microsoft.EntityFrameworkCore;
using Quartz;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Jobs;

public sealed class GenerateDatesJob : IJob
{
    private readonly IScheduleDbContext _context;
    private readonly IDateInfoService _dateInfoService;

    public GenerateDatesJob(IScheduleDbContext context,
        IDateInfoService dateInfoService)
    {
        _context = context;
        _dateInfoService = dateInfoService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var lastDate = await _context.Set<Date>()
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(e => e.DateId)
            .LastOrDefaultAsync();

        if (lastDate is null)
            throw new NotFoundException(nameof(Date));

        var currentDate = _dateInfoService.CurrentDate;

        while (IsNeedGenerate(lastDate.Value, currentDate.Value))
        {
            var nextDate = _dateInfoService.GetNextDate(lastDate.Value);
            await _context.Set<Date>().AddAsync(nextDate);
            lastDate = nextDate;
        }

        await _context.SaveChangesAsync();
    }
    
    public bool IsNeedGenerate(DateTime last, DateTime now)
    {
        var currentWeek = _dateInfoService.GetWeekOfYear(now);
        var lastWeek = _dateInfoService.GetWeekOfYear(last);

        if (lastWeek < currentWeek && last.Year == now.Year)
            return true;
        if (lastWeek == currentWeek)
            return true;
        return currentWeek == 1 && lastWeek == _dateInfoService.MaxWeekOfYear;
    }
}