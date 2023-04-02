using AutoMapper;
using MediatR;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.SpecialityCodes.Commands.Create;

public sealed class CreateSpecialityCodeCommandHandler : IRequestHandler<CreateSpecialityCodeCommand, int>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateSpecialityCodeCommandHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateSpecialityCodeCommand request, CancellationToken cancellationToken)
    {
        var specialityCode = _mapper.Map<SpecialityCode>(request);
        await _context.SpecialityCodes.AddAsync(specialityCode, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return specialityCode.SpecialityCodeId;
    }
}