using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineTypes.Queries.Get;

public sealed class GetDisciplineTypeQueryHandler : IRequestHandler<GetDisciplineTypeQuery, DisciplineTypeViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetDisciplineTypeQueryHandler(IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<DisciplineTypeViewModel> Handle(GetDisciplineTypeQuery request,
        CancellationToken cancellationToken)
    {
        var discipline = await _context.Set<DisciplineType>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DisciplineTypeId == request.Id, cancellationToken);

        if (discipline is null)
            throw new NotFoundException(nameof(DisciplineType), request.Id);

        return _mapper.Map<DisciplineTypeViewModel>(discipline);
    }
}