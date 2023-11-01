using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineNames.Queries.Get;

public sealed class GetDisciplineNameQueryHandler : IRequestHandler<GetDisciplineNameQuery, DisciplineNameViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetDisciplineNameQueryHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<DisciplineNameViewModel> Handle(GetDisciplineNameQuery request,
        CancellationToken cancellationToken)
    {
        var discipline = await _context.Set<DisciplineName>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DisciplineNameId == request.Id, cancellationToken);

        if (discipline is null)
            throw new NotFoundException(nameof(DisciplineName), request.Id);

        return _mapper.Map<DisciplineNameViewModel>(discipline);
    }
}