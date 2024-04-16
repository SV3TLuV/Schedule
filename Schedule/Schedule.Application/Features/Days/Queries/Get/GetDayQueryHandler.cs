using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Days.Queries.Get;

public sealed class GetDayQueryHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<GetDayQuery, DayViewModel>
{
    public async Task<DayViewModel> Handle(GetDayQuery request, CancellationToken cancellationToken)
    {
        var day = await context.Days
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.DayId == request.Id, cancellationToken);

        if (day is null)
            throw new NotFoundException(nameof(Day), request.Id);

        return mapper.Map<DayViewModel>(day);
    }
}