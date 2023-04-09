using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.SpecialityCodes.Queries.Get;

public sealed class GetSpecialityCodeQueryHandler : IRequestHandler<GetSpecialityCodeQuery, SpecialityCodeViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetSpecialityCodeQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SpecialityCodeViewModel> Handle(GetSpecialityCodeQuery request,
        CancellationToken cancellationToken)
    {
        var specialityCode = await _context.Set<SpecialityCode>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.SpecialityCodeId == request.Id, cancellationToken);

        if (specialityCode is null)
            throw new NotFoundException(nameof(SpecialityCode), request.Id);

        return _mapper.Map<SpecialityCodeViewModel>(specialityCode);
    }
}