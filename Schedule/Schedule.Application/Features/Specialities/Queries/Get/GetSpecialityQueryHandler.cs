using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Queries.Get;

public sealed class GetSpecialityQueryHandler : IRequestHandler<GetSpecialityQuery, SpecialityViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetSpecialityQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SpecialityViewModel> Handle(GetSpecialityQuery request,
        CancellationToken cancellationToken)
    {
        var speciality = await _context.Set<Speciality>()
            .Include(e => e.Disciplines)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.SpecialityId == request.Id, cancellationToken);

        if (speciality is null)
            throw new NotFoundException(nameof(Speciality), request.Id);

        return _mapper.Map<SpecialityViewModel>(speciality);
    }
}