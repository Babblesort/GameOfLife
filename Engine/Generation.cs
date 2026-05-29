namespace Engine;

public class Generation(int rows, int cols)
{
    private readonly bool[] _cells = new bool[rows * cols];
    private readonly int _rows = rows;
    private readonly int _cols = cols;
    public bool HasLiveCells => Array.IndexOf(_cells, true) >= 0;
    public int Count => _cells.Length;
    public int Rows => _rows;
    public int Cols => _cols;

    public bool this[RowCol key]
    {
        get => _cells[key.Row * _cols + key.Col];
        set => _cells[key.Row * _cols + key.Col] = value;
    }

    public IEnumerable<string> ToCsv()
    {
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cols; c++)
            {
                yield return $"{r},{c},{_cells[r * _cols + c]}";
            }
        }
    }

    public void CopyTo(bool[] dest) => Array.Copy(_cells, dest, _cells.Length);

    internal bool[] Raw => _cells;

    public override bool Equals(object? obj)
    {
        if (obj is not Generation other) { return false; }
        if (_rows != other._rows || _cols != other._cols) { return false; }

        return _cells.SequenceEqual(other._cells);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(_rows);
        hash.Add(_cols);
        foreach (bool cell in _cells)
        {
            hash.Add(cell);
        }
        return hash.ToHashCode();
    }
}
