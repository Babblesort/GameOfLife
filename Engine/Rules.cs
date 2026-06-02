namespace Engine;

public class Rules
{
    public const int MinNeighbors = 0;
    public const int MaxNeighbors = 8;
    public const int MinCount = 1;
    public const int MaxCount = 8;
    public IReadOnlyList<int> SurviveNeighborCounts { get; }
    public IReadOnlyList<int> BirthNeighborCounts { get; }
    private static readonly int[] DefaultSurviveNeighborCounts = [2, 3];
    private static readonly int[] DefaultBirthNeighborCounts = [3];
    internal int SurviveMask { get; }
    internal int BirthMask { get; }

    public Rules() : this(DefaultSurviveNeighborCounts, DefaultBirthNeighborCounts) { }

    public Rules(IList<int> surviveCounts, IList<int> birthCounts)
    {
        ThrowOnInvalidCounts(surviveCounts, birthCounts);
        SurviveNeighborCounts = [.. surviveCounts];
        BirthNeighborCounts = [.. birthCounts];
        SurviveMask = BuildMask(surviveCounts);
        BirthMask = BuildMask(birthCounts);
    }

    private static int BuildMask(IList<int> counts)
    {
        int mask = 0;
        foreach (var n in counts)
        {
            mask |= 1 << n;
        }
        return mask;
    }

    private static void ThrowOnInvalidCounts(IList<int> surviveCounts, IList<int> birthCounts)
    {
        ArgumentNullException.ThrowIfNull(surviveCounts);
        ArgumentNullException.ThrowIfNull(birthCounts);

        if (HasInvalidItemCount(surviveCounts)) throw new ArgumentOutOfRangeException(nameof(surviveCounts), $"Must have {MinCount} to {MaxCount} items");
        if (HasInvalidItemCount(birthCounts)) throw new ArgumentOutOfRangeException(nameof(birthCounts), $"Must have {MinCount} to {MaxCount} items");

        if (HasOutOfRangeValues(surviveCounts)) throw new ArgumentOutOfRangeException(nameof(surviveCounts), $"Must have only items from {MinNeighbors} to {MaxNeighbors} inclusive");
        if (HasOutOfRangeValues(birthCounts)) throw new ArgumentOutOfRangeException(nameof(birthCounts), $"Must have only items from {MinNeighbors} to {MaxNeighbors} inclusive");
    }

    private static bool HasInvalidItemCount(IList<int> list) => list.Count < MinCount || list.Count > MaxCount;
    private static bool HasOutOfRangeValues(IList<int> list) => list.Any(i => i > MaxNeighbors || i < MinNeighbors);
}
