using AutoMapper;
using MediatR;
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
        var speciality = _mapper.Map<Speciality>(request);
        await _context.Set<Speciality>().AddAsync(speciality, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return speciality.SpecialityId;
    }
}