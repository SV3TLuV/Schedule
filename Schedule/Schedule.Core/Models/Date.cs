namespace Schedule.Core.Models;

public sealed record Date
{
    public int DayId { get; set; }
    public int WeekTypeId { get; set; }
    public int Term { get; set; }
}