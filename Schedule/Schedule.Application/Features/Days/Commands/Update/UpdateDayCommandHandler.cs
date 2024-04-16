using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Days.Commands.Update;

public sealed class UpdateDayCommandHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<UpdateDayCommand, Unit>
{
    public async Task<Unit> Handle(UpdateDayCommand request, CancellationToken cancellationToken)
    {
        var dayDbo = await context.Days
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DayId == request.Id, cancellationToken);

        if (dayDbo is null)
            throw new NotFoundException(nameof(Day), request.Id);

        var day = mapper.Map<Day>(request);
        context.Days.Update(day);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}