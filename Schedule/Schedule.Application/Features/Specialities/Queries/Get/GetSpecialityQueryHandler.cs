using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Queries.Get;

public sealed class GetSpecialityQueryHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<GetSpecialityQuery, SpecialityViewModel>
{
    public async Task<SpecialityViewModel> Handle(GetSpecialityQuery request,
        CancellationToken cancellationToken)
    {
        var speciality = await context.Specialities
            .Include(e => e.Disciplines)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.SpecialityId == request.Id, cancellationToken);

        if (speciality is null)
            throw new NotFoundException(nameof(Speciality), request.Id);

        return mapper.Map<SpecialityViewModel>(speciality);
    }
}