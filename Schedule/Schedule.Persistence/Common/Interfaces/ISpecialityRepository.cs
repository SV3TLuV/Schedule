using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface ISpecialityRepository
{
    public Task<int> CreateAsync(Speciality speciality, CancellationToken cancellationToken = default);

    public Task UpdateAsync(Speciality speciality, CancellationToken cancellationToken = default);
    
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    
    public Task RestoreAsync(int id, CancellationToken cancellationToken = default);
}