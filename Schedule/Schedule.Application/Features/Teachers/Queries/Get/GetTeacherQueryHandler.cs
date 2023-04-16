using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Queries.Get;

public sealed class GetTeacherQueryHandler : IRequestHandler<GetTeacherQuery, TeacherViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetTeacherQueryHandler(IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TeacherViewModel> Handle(GetTeacherQuery request,
        CancellationToken cancellationToken)
    {
        var teacher = await _context.Set<Teacher>()
            .Include(e => e.Groups)
            .Include(e => e.Disciplines)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TeacherId == request.Id, cancellationToken);
        return _mapper.Map<TeacherViewModel>(teacher);
    }
}