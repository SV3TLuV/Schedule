using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Dates.Queries.Get;

public sealed class GetDateQueryHandler : IRequestHandler<GetDateQuery, DateViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetDateQueryHandler(IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DateViewModel> Handle(GetDateQuery request,
        CancellationToken cancellationToken)
    {
        var date = await _context.Set<Date>()
            .Include(e => e.Day)
            .Include(e => e.WeekType)
            .Include(e => e.TimeType)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DateId == request.Id, cancellationToken);

        if (date is null)
            throw new NotFoundException(nameof(Date), request.Id);

        return _mapper.Map<DateViewModel>(date);
    }
}