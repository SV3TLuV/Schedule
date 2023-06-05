using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Days.Commands.Update;

public sealed class UpdateDayCommandHandler : IRequestHandler<UpdateDayCommand, Unit>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateDayCommandHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateDayCommand request, CancellationToken cancellationToken)
    {
        var dayDbo = await _context.Set<Day>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DayId == request.Id, cancellationToken);

        if (dayDbo is null)
            throw new NotFoundException(nameof(Day), request.Id);

        var day = _mapper.Map<Day>(request);
        _context.Set<Day>().Update(day);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}