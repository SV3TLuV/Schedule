using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class ClassroomViewModel : IMapWith<Classroom>
{
    public int Id { get; set; }

    public string Cabinet { get; set; } = null!;

    public bool IsDeleted { get; set; }
}