namespace Schedule.Core.Common.Interfaces;

public interface IDateInfoService : IWeekInfoService, IDayInfoService
{
    int CurrentTerm { get; }

    DateTime CurrentDateTime { get; }

    int GetTerm(DateTime dateTime);
}