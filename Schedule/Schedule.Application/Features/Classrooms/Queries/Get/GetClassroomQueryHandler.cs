using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Classrooms.Queries.Get;

public sealed class GetClassroomQueryHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<GetClassroomQuery, ClassroomViewModel>
{
    public async Task<ClassroomViewModel> Handle(GetClassroomQuery request,
        CancellationToken cancellationToken)
    {
        var classroom = await context.Classrooms
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.ClassroomId == request.Id, cancellationToken);

        if (classroom is null)
            throw new NotFoundException(nameof(Classroom), request.Id);

        return mapper.Map<ClassroomViewModel>(classroom);
    }
}