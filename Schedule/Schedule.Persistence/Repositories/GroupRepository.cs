using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class GroupRepository(
    IScheduleDbContext context,
    IDateInfoService dateInfoService) : IGroupRepository
{
    public async Task<int> CreateAsync(Group group, CancellationToken cancellationToken = default)
    {
        int id;

        var groupDb = await context.Groups.FirstOrDefaultAsync(e =>
            e.SpecialityId == group.SpecialityId &&
            e.EnrollmentYear == group.EnrollmentYear &&
            e.Number == group.Number, cancellationToken);

        if (groupDb is null)
        {
            var speciality = await context.Specialities.FirstOrDefaultAsync(e =>
                e.SpecialityId == group.SpecialityId, cancellationToken);

            if (speciality is null)
            {
                throw new NotFoundException(nameof(Speciality), group.SpecialityId);
            }

            group.TermId = group.CalculateTerm(dateInfoService);
            group.Name = $"{speciality.Name}-{group.Number}";

            var created = await context.Groups.AddAsync(group, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            id = created.Entity.GroupId;
        }
        else if (groupDb.IsDeleted)
        {
            groupDb.IsDeleted = false;
            context.Groups.Update(groupDb);

            await context.SaveChangesAsync(cancellationToken);

            id = groupDb.GroupId;
        }
        else
        {
            throw new AlreadyExistsException(groupDb.Name);
        }

        return id;
    }

    public async Task UpdateAsync(Group group, CancellationToken cancellationToken = default)
    {
        var groupDb = await context.Groups.FirstOrDefaultAsync(e =>
            e.GroupId == group.GroupId, cancellationToken);

        if (groupDb is null)
        {
            throw new NotFoundException(nameof(Group), group.GroupId);
        }

        var search = await context.Groups
            .Include(e => e.Speciality)
            .FirstOrDefaultAsync(e =>
                e.SpecialityId == group.SpecialityId &&
                e.EnrollmentYear == group.EnrollmentYear &&
                e.Number == group.Number &&
                e.GroupId != groupDb.GroupId, cancellationToken);

        if (search is not null)
        {
            throw new AlreadyExistsException(group.Name);
        }

        var speciality = await context.Specialities.FirstOrDefaultAsync(e =>
            e.SpecialityId == group.SpecialityId, cancellationToken);

        if (speciality is null)
        {
            throw new NotFoundException(nameof(Speciality), group.SpecialityId);
        }

        groupDb.EnrollmentYear = group.EnrollmentYear;
        groupDb.IsAfterEleven = group.IsAfterEleven;
        groupDb.SpecialityId = group.SpecialityId;
        groupDb.Number = group.Number;
        groupDb.TermId = groupDb.CalculateTerm(dateInfoService);
        groupDb.Name = $"{speciality.Name}-{group.Number}";

        context.Groups.Update(groupDb);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var group = await context.Groups.FirstOrDefaultAsync(e => e.GroupId == id, cancellationToken);

        if (group is null)
        {
            throw new NotFoundException(nameof(Group), id);
        }

        group.IsDeleted = true;

        context.Groups.Update(group);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task RestoreAsync(int id, CancellationToken cancellationToken = default)
    {
        var group = await context.Groups.FirstOrDefaultAsync(e => e.GroupId == id, cancellationToken);

        if (group is null)
        {
            throw new NotFoundException(nameof(Group), id);
        }

        group.IsDeleted = false;

        context.Groups.Update(group);

        await context.SaveChangesAsync(cancellationToken);
    }
}