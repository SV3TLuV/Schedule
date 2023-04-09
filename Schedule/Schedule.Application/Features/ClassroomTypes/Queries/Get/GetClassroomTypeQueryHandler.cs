using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.ClassroomTypes.Queries.Get;

public sealed class GetClassroomTypeQueryHandler : IRequestHandler<GetClassroomTypeQuery, ClassroomTypeViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetClassroomTypeQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ClassroomTypeViewModel> Handle(GetClassroomTypeQuery request,
        CancellationToken cancellationToken)
    {
        var classroomType = await _context.Set<ClassroomType>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.ClassroomTypeId == request.Id, cancellationToken);

        if (classroomType is null)
            throw new NotFoundException(nameof(ClassroomType), request.Id);

        return _mapper.Map<ClassroomTypeViewModel>(classroomType);
    }
}