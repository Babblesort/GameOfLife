namespace Engine;

public class Rules
{
    public const int MinNeighbors = 0;
    public const int MaxNeighbors = 8;
    public const int MinCount = 1;
    public const int MaxCount = 8;
    public IList<int> SurviveNeighborCounts { get; }
    public IList<int> BirthNeighborCounts { get; }

    private static readonly List<int> DefaultSurviveNeighborCounts = [2, 3];
    private static readonly List<int> DefaultBirthNeighborCounts = [3];

    public Rules() : this(DefaultSurviveNeighborCounts, DefaultBirthNeighborCounts) { }

    internal int SurviveMask { get; }
    internal int BirthMask { get; }

    public Rules(IList<int> surviveCounts, IList<int> birthCounts)
    {
        ThrowOnInvalidCounts(surviveCounts, birthCounts);
        SurviveNeighborCounts = surviveCounts;
        BirthNeighborCounts = birthCounts;
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
        if (surviveCounts == null) throw new ArgumentNullException(nameof(surviveCounts), "Must not be null");
        if (birthCounts == null) throw new ArgumentNullException(nameof(birthCounts), "Must not be null");

        if (InvalidNeighborCountItems(surviveCounts)) throw new ArgumentOutOfRangeException(nameof(surviveCounts), $"Must have {MinCount} to {MaxCount} items");
        if (InvalidNeighborCountItems(birthCounts)) throw new ArgumentOutOfRangeException(nameof(birthCounts), $"Must have {MinCount} to {MaxCount} items");

        if (InvalidNeighborCount(surviveCounts)) throw new ArgumentOutOfRangeException(nameof(surviveCounts), $"Must have only items from {MinNeighbors} to {MaxNeighbors} inclusive");
        if (InvalidNeighborCount(birthCounts)) throw new ArgumentOutOfRangeException(nameof(birthCounts), $"Must have only items from {MinNeighbors} to {MaxNeighbors} inclusive");
    }

    private static bool InvalidNeighborCountItems(IList<int> list) => list.Count < MinCount || list.Count > MaxCount;
    private static bool InvalidNeighborCount(IList<int> list) => list.Any(i => i > MaxNeighbors || i < MinNeighbors);
}
