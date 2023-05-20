using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Terms.Queries.GetAll;

public sealed class GetTermListQueryHandler : IRequestHandler<GetTermListQuery, PagedList<TermViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetTermListQueryHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PagedList<TermViewModel>> Handle(GetTermListQuery request,
        CancellationToken cancellationToken)
    {
        var terms = await _context.Set<Term>()
            .AsNoTrackingWithIdentityResolution()
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var viewModels = _mapper.Map<List<TermViewModel>>(terms);
        var totalCount = await _context.Set<Term>().CountAsync(cancellationToken);

        return new PagedList<TermViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}