using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.SpecialityCodes.Commands.Create;

public sealed class CreateSpecialityCodeCommandHandler : IRequestHandler<CreateSpecialityCodeCommand, int>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateSpecialityCodeCommandHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateSpecialityCodeCommand request, CancellationToken cancellationToken)
    {
        var specialityCode = _mapper.Map<SpecialityCode>(request);
        await _context.Set<SpecialityCode>().AddAsync(specialityCode, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return specialityCode.SpecialityCodeId;
    }
}