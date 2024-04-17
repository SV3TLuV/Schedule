using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineTypes.Queries.Get;

public sealed class GetDisciplineTypeQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetDisciplineTypeQuery, DisciplineTypeViewModel>
{
    public async Task<DisciplineTypeViewModel> Handle(GetDisciplineTypeQuery request,
        CancellationToken cancellationToken)
    {
        var discipline = await context.DisciplineTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.DisciplineTypeId == request.Id, cancellationToken);

        if (discipline is null)
            throw new NotFoundException(nameof(DisciplineType), request.Id);

        return mapper.Map<DisciplineTypeViewModel>(discipline);
    }
}