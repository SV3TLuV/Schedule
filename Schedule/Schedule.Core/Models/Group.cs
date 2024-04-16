using Schedule.Core.Common.Interfaces;

namespace Schedule.Core.Models;

public class Group
{
    public int GroupId { get; set; }

    public string Number { get; set; } = null!;

    public int SpecialityId { get; set; }

    public int TermId { get; set; }
    
    public bool IsAfterEleven { get; set; }

    public int EnrollmentYear { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<GroupTransfer> GroupTransfers { get; set; } = new List<GroupTransfer>();

    public virtual Speciality Speciality { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual Term Term { get; set; } = null!;

    public virtual Timetable? Timetable { get; set; }

    public int CalculateTerm(IDateInfoService dateInfoService)
    {
        int term;

        if (dateInfoService.CurrentTerm == 2)
        {
            term = dateInfoService.CurrentTerm + 2 * (dateInfoService.CurrentDateTime.Year - EnrollmentYear - 1);
        }
        else
        {
            term = dateInfoService.CurrentTerm + 2 * (dateInfoService.CurrentDateTime.Year - EnrollmentYear);
        }

        return IsAfterEleven ? term + 2 : term;
    }
}