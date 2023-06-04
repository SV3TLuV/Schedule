using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Dates.Queries.GetCurrent;

public sealed class GetCurrentDateQueryHandler : IRequestHandler<GetCurrentDateQuery, DateViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IDateInfoService _dateInfoService;
    private readonly IMapper _mapper;

    public GetCurrentDateQueryHandler(IScheduleDbContext context,
        IDateInfoService dateInfoService,
        IMapper mapper)
    {
        _context = context;
        _dateInfoService = dateInfoService;
        _mapper = mapper;
    }

    public async Task<DateViewModel> Handle(GetCurrentDateQuery request,
        CancellationToken cancellationToken)
    {
        var currentDate = _dateInfoService.CurrentDate;
        var date = await _context.Set<Date>()
            .Include(e => e.Day)
            .Include(e => e.WeekType)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.Value.Date == currentDate.Value.Date,
                cancellationToken);

        if (date is null)
            throw new NotFoundException(nameof(Date), currentDate.Value.Date);

        return _mapper.Map<DateViewModel>(date);
    }
}