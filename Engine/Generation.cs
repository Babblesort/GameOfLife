namespace Engine;

public class Generation(int rows, int cols)
{
    private readonly bool[] _cells = new bool[rows * cols];
    public bool HasLiveCells => _cells.AsSpan().Contains(true);
    public int Count => _cells.Length;
    public int Rows { get; } = rows;
    public int Cols { get; } = cols;

    public bool this[RowCol key]
    {
        get => _cells[key.Row * Cols + key.Col];
        set => _cells[key.Row * Cols + key.Col] = value;
    }

    public IEnumerable<string> ToCsv()
    {
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Cols; c++)
            {
                yield return $"{r},{c},{_cells[r * Cols + c]}";
            }
        }
    }

    public void CopyTo(bool[] dest) => Array.Copy(_cells, dest, _cells.Length);

    public void ResolveNextGeneration(Rules rules, Generation destination)
    {
        bool[] src = _cells;
        bool[] dst = destination._cells;

        for (int r = 0; r < Rows; r++)
        {
            //account for grid wrap by treating the first row as a neighbor of the last and vice versa
            int up = r == 0 ? Rows - 1 : r - 1;
            int down = r == Rows - 1 ? 0 : r + 1;

            for (int c = 0; c < Cols; c++)
            {
                //account for grid wrap for cols as well
                int left = c == 0 ? Cols - 1 : c - 1;
                int right = c == Cols - 1 ? 0 : c + 1;

                int n = 0;
                if (src[up * Cols + left]) { n++; }
                if (src[up * Cols + c]) { n++; }
                if (src[up * Cols + right]) { n++; }
                if (src[r * Cols + left]) { n++; }
                if (src[r * Cols + right]) { n++; }
                if (src[down * Cols + left]) { n++; }
                if (src[down * Cols + c]) { n++; }
                if (src[down * Cols + right]) { n++; }

                bool alive = src[r * Cols + c];
                dst[r * Cols + c] = alive
                    ? IsBitSet(rules.SurviveMask, n)
                    : IsBitSet(rules.BirthMask, n);
            }
        }
    }

    private static bool IsBitSet(int mask, int neighborCount) => ((mask >> neighborCount) & 1) != 0;

    public override bool Equals(object? obj)
    {
        if (obj is not Generation other) { return false; }
        if (Rows != other.Rows || Cols != other.Cols) { return false; }

        return _cells.AsSpan().SequenceEqual(other._cells);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(Rows);
        hash.Add(Cols);
        foreach (bool cell in _cells)
        {
            hash.Add(cell);
        }
        return hash.ToHashCode();
    }
}
