using Schedule.Application.ViewModels;

namespace Schedule.Application.Common.EqualityComparers;

public sealed class GroupViewModelsEqualityComparer : IEqualityComparer<ICollection<GroupViewModel>>
{
    public bool Equals(ICollection<GroupViewModel> x, ICollection<GroupViewModel> y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return CollectionsEquals(x, y);
    }

    public int GetHashCode(ICollection<GroupViewModel> obj)
    {
        return HashCode.Combine(obj.Count, obj.IsReadOnly);
    }

    private bool CollectionsEquals(ICollection<GroupViewModel> x, ICollection<GroupViewModel> y)
    {
        var xIds = x.Select(e => e.Id)
            .Order()
            .ToArray();
        var yIds = y.Select(e => e.Id)
            .Order()
            .ToArray();
        return xIds.SequenceEqual(yIds);
    }
}