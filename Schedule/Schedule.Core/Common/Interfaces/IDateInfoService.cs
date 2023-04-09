using Schedule.Core.Models;

namespace Schedule.Core.Common.Interfaces;

public interface IDateInfoService : IWeekInfoService, IDayInfoService
{
    int CurrentTerm { get; }

    Date CurrentDate { get; }

    Date GetDate(DateTime dateTime);
    
    Date GetNextDate(DateTime dateTime);

    int GetTerm(DateTime dateTime);
}