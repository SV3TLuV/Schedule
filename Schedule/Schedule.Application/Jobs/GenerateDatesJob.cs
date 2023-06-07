using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.Features.Dates.Commands.Create;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Jobs;

public sealed class GenerateDatesJob : IJob
{
    private readonly IScheduleDbContext _context;
    private readonly IDateInfoService _dateInfoService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GenerateDatesJob(IScheduleDbContext context,
        IDateInfoService dateInfoService,
        IMediator mediator,
        IMapper mapper)
    {
        _context = context;
        _dateInfoService = dateInfoService;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var days = await _context.Set<Day>()
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();

        var lastDate = await _context.Set<Date>()
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(e => e.DateId)
            .LastOrDefaultAsync();

        if (lastDate is null)
        {
            var tomorrow = _dateInfoService.CurrentDateTime.AddDays(-1);
            lastDate = _dateInfoService.GetDate(tomorrow);
        }

        var currentDate = _dateInfoService.CurrentDate;

        while (IsNeedGenerate(lastDate.Value, currentDate.Value))
        {
            var nextDate = _dateInfoService.GetNextDate(lastDate.Value);
            var day = days.First(e => e.DayId == nextDate.DayId);

            nextDate.IsStudy = day.IsStudy;

            var command = _mapper.Map<CreateDateCommand>(nextDate);
            await _mediator.Send(command);

            lastDate = nextDate;
        }
    }

    public bool IsNeedGenerate(DateTime last, DateTime now)
    {
        var currentWeek = _dateInfoService.GetWeekOfYear(now);
        var lastWeek = _dateInfoService.GetWeekOfYear(last);

        if (last.Year < now.Year)
            return true;
        if (lastWeek < currentWeek && last.Year == now.Year)
            return true;
        if (lastWeek == currentWeek)
            return true;
        if (lastWeek == currentWeek + 1)
            return true;
        return currentWeek == 1 && lastWeek == _dateInfoService.MaxWeekOfYear;
    }
}