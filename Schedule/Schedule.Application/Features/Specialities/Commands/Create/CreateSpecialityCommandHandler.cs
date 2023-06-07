using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Commands.Create;

public sealed class CreateSpecialityCommandHandler : IRequestHandler<CreateSpecialityCommand, int>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateSpecialityCommandHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateSpecialityCommand request, CancellationToken cancellationToken)
    {
        var searched = await _context.Set<Speciality>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e =>
                e.Name == request.Name &&
                e.Code == request.Code, cancellationToken);

        if (searched is not null)
            throw new AlreadyExistsException($"Специальность: {searched.Name}");
        
        var speciality = _mapper.Map<Speciality>(request);
        await _context.Set<Speciality>().AddAsync(speciality, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return speciality.SpecialityId;
    }
}