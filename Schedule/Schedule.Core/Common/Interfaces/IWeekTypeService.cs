namespace Schedule.Core.Common.Interfaces;

public interface IWeekTypeService
{
    int GetCurrentWeekTypeId();
    int GetAnotherWeekTypeId(int id);
}