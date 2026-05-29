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

    internal bool[] Raw => _cells;

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
