﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Classrooms.Commands.Update;

public sealed class UpdateClassroomCommandHandler : IRequestHandler<UpdateClassroomCommand, Unit>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateClassroomCommandHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateClassroomCommand request, CancellationToken cancellationToken)
    {
        var classroomDbo = await _context.Set<Classroom>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.ClassroomId == request.Id, cancellationToken);

        if (classroomDbo is null)
            throw new NotFoundException(nameof(Classroom), request.Id);

        var classroom = _mapper.Map<Classroom>(request);
        _context.Set<Classroom>().Update(classroom);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}