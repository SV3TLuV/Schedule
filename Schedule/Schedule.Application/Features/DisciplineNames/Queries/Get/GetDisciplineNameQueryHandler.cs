using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineNames.Queries.Get;

public sealed class GetDisciplineNameQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetDisciplineNameQuery, DisciplineNameViewModel>
{
    public async Task<DisciplineNameViewModel> Handle(GetDisciplineNameQuery request,
        CancellationToken cancellationToken)
    {
        var discipline = await context.DisciplineNames
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DisciplineNameId == request.Id, cancellationToken);

        if (discipline is null)
            throw new NotFoundException(nameof(DisciplineName), request.Id);

        return mapper.Map<DisciplineNameViewModel>(discipline);
    }
}