using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Classrooms.Queries.Get;

public sealed class GetClassroomQueryHandler : IRequestHandler<GetClassroomQuery, ClassroomViewModel>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetClassroomQueryHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ClassroomViewModel> Handle(GetClassroomQuery request,
        CancellationToken cancellationToken)
    {
        var classroom = await _context.Classrooms
            .Include(e => e.ClassroomTypes)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.ClassroomId == request.Id, cancellationToken);

        if (classroom is null)
            throw new NotFoundException(nameof(Classroom), request.Id);

        return _mapper.Map<ClassroomViewModel>(classroom);
    }
}