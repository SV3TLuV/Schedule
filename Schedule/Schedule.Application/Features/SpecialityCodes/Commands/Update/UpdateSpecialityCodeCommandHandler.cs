using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.SpecialityCodes.Commands.Update;

public sealed class UpdateSpecialityCodeCommandHandler : IRequestHandler<UpdateSpecialityCodeCommand>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateSpecialityCodeCommandHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateSpecialityCodeCommand request, CancellationToken cancellationToken)
    {
        var specialityCodeDbo = await _context.SpecialityCodes
            .FirstOrDefaultAsync(e => e.SpecialityCodeId == request.Id, cancellationToken);
        
        if (specialityCodeDbo is null)
            throw new NotFoundException(nameof(SpecialityCode), request.Id);
        
        var specialityCode = _mapper.Map<SpecialityCode>(request);
        _context.SpecialityCodes.Update(specialityCode);
        await _context.SaveChangesAsync(cancellationToken);
    }
}