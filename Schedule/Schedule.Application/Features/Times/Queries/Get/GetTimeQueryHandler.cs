using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Times.Queries.Get;

public sealed class GetTimeQueryHandler : IRequestHandler<GetTimeQuery, TimeViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetTimeQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TimeViewModel> Handle(GetTimeQuery request, CancellationToken cancellationToken)
    {
        var time = await _context.Set<Time>()
            .Include(e => e.Type)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TimeId == request.Id, cancellationToken);

        if (time is null)
            throw new NotFoundException(nameof(Time), request.Id);

        return _mapper.Map<TimeViewModel>(time);
    }
}