﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.WeekTypes.Queries.Get;

public sealed class GetWeekTypeQueryHandler : IRequestHandler<GetWeekTypeQuery, WeekTypeViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetWeekTypeQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<WeekTypeViewModel> Handle(GetWeekTypeQuery request, CancellationToken cancellationToken)
    {
        var weekType = await _context.Set<WeekType>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.WeekTypeId == request.Id, cancellationToken);

        if (weekType is null)
            throw new NotFoundException(nameof(WeekType), request.Id);

        return _mapper.Map<WeekTypeViewModel>(weekType);
    }
}