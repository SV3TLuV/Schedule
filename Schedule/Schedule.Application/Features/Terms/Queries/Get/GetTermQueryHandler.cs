using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Terms.Queries.Get;

public sealed class GetTermQueryHandler : IRequestHandler<GetTermQuery, TermViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetTermQueryHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<TermViewModel> Handle(GetTermQuery request,
        CancellationToken cancellationToken)
    {
        var term = await _context.Set<Term>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TermId == request.Id, cancellationToken);

        if (term is null)
            throw new NotFoundException(nameof(Term), request.Id);

        return _mapper.Map<TermViewModel>(term);
    }
}