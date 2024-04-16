using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Terms.Queries.Get;

public sealed class GetTermQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetTermQuery, TermViewModel>
{
    public async Task<TermViewModel> Handle(GetTermQuery request,
        CancellationToken cancellationToken)
    {
        var term = await context.Terms
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TermId == request.Id, cancellationToken);

        if (term is null)
            throw new NotFoundException(nameof(Term), request.Id);

        return mapper.Map<TermViewModel>(term);
    }
}