using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class GroupRepository(
    IScheduleDbContext context,
    IDateInfoService dateInfoService) : Repository(context), IGroupRepository
{
    public async Task<int> CreateAsync(Group group, CancellationToken cancellationToken = default)
    {
        group.TermId = group.CalculateTerm(dateInfoService);

        var created = await Context.Groups.AddAsync(group, cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);

        return created.Entity.GroupId;
    }

    public async Task UpdateAsync(Group group, CancellationToken cancellationToken = default)
    {
        var groupDb = await Context.Groups.FirstOrDefaultAsync(e => e.GroupId == group.GroupId, cancellationToken);

        if (groupDb is null)
        {
            throw new NotFoundException(nameof(Group), group.GroupId);
        }

        groupDb.EnrollmentYear = group.EnrollmentYear;
        groupDb.IsAfterEleven = group.IsAfterEleven;
        groupDb.SpecialityId = group.SpecialityId;
        groupDb.Number = group.Number;

        groupDb.TermId = groupDb.CalculateTerm(dateInfoService);

        Context.Groups.Update(groupDb);

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var group = await Context.Groups.FirstOrDefaultAsync(e => e.GroupId == id, cancellationToken);

        if (group is null)
        {
            throw new NotFoundException(nameof(Group), id);
        }

        group.IsDeleted = true;

        Context.Groups.Update(group);

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task RestoreAsync(int id, CancellationToken cancellationToken = default)
    {
        var group = await Context.Groups.FirstOrDefaultAsync(e => e.GroupId == id, cancellationToken);

        if (group is null)
        {
            throw new NotFoundException(nameof(Group), id);
        }

        group.IsDeleted = false;

        Context.Groups.Update(group);

        await Context.SaveChangesAsync(cancellationToken);
    }
}