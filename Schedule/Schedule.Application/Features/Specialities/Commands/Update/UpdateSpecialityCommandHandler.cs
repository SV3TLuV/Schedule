using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Commands.Update;

public sealed class UpdateSpecialityCommandHandler : IRequestHandler<UpdateSpecialityCommand>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateSpecialityCommandHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateSpecialityCommand request, CancellationToken cancellationToken)
    {
        var specialityDbo = await _context.Set<Speciality>()
            .FirstOrDefaultAsync(e => e.SpecialityId == request.Id, cancellationToken);

        if (specialityDbo is null)
            throw new NotFoundException(nameof(Speciality), request.Id);

        var speciality = _mapper.Map<Speciality>(request);
        _context.Set<Speciality>().Update(speciality);
        await _context.SaveChangesAsync(cancellationToken);
    }
}