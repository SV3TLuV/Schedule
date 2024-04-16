using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.WeekTypes.Queries.Get;

public sealed class GetWeekTypeQueryHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<GetWeekTypeQuery, WeekTypeViewModel>
{
    public async Task<WeekTypeViewModel> Handle(GetWeekTypeQuery request, CancellationToken cancellationToken)
    {
        var weekType = await context.WeekTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.WeekTypeId == request.Id, cancellationToken);

        if (weekType is null)
            throw new NotFoundException(nameof(WeekType), request.Id);

        return mapper.Map<WeekTypeViewModel>(weekType);
    }
}