using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.ClassroomTypes.Queries.Get;

public sealed class GetClassroomTypeQueryHandler : IRequestHandler<GetClassroomTypeQuery, ClassroomTypeViewModel>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetClassroomTypeQueryHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ClassroomTypeViewModel> Handle(GetClassroomTypeQuery request,
        CancellationToken cancellationToken)
    {
        var classroomType = await _context.ClassroomTypes
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.ClassroomTypeId == request.Id, cancellationToken);

        if (classroomType is null)
            throw new NotFoundException(nameof(ClassroomType), request.Id);

        return _mapper.Map<ClassroomTypeViewModel>(classroomType);
    }
}