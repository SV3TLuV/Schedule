using Schedule.Core.Models;

namespace Schedule.Core.Common.Interfaces;

public interface IDateService
{
    Date GetPreviousDate(Date date);
    Date GetCurrentDate();
    Date GetNextDate(Date date);
}