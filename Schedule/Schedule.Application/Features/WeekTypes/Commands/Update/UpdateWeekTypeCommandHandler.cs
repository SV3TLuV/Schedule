using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.WeekTypes.Commands.Update;

public sealed class UpdateWeekTypeCommandHandler : IRequestHandler<UpdateWeekTypeCommand>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateWeekTypeCommandHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateWeekTypeCommand request, CancellationToken cancellationToken)
    {
        var weekTypeDbo = await _context.WeekTypes
            .FirstOrDefaultAsync(e => e.WeekTypeId == request.Id, cancellationToken);
        
        if (weekTypeDbo is null)
            throw new NotFoundException(nameof(WeekType), request.Id);
        
        var weekType = _mapper.Map<WeekType>(request);
        _context.WeekTypes.Update(weekType);
        await _context.SaveChangesAsync(cancellationToken);
    }
}