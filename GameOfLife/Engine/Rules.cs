using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public class Rules
    {
        public static int MinNeighbors = 0;
        public static int MaxNeighbors = 8;
        public static int MinCount = 1;
        public static int MaxCount = 8;
        public IList<int> SurviveNeighborCounts { get; }
        public IList<int> BirthNeighborCounts { get; }

        private static List<int> DefaultSurviveNeighborCounts = new List<int> { 2, 3 };
        private static List<int> DefaultBirthNeighborCounts = new List<int> { 3 };

        public Rules() : this(DefaultSurviveNeighborCounts, DefaultBirthNeighborCounts) { }

        public Rules(IList<int> surviveCounts, IList<int> birthCounts)
        {
            ValidateCounts(surviveCounts, birthCounts);
            SurviveNeighborCounts = surviveCounts;
            BirthNeighborCounts = birthCounts;
        }

        private static void ValidateCounts(IList<int> surviveCounts, IList<int> birthCounts)
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
}
