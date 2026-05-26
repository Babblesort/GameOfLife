using System.Collections;

namespace Engine;

public class Generation : IEnumerable<KeyValuePair<RowCol, bool>>
{
    private readonly bool[] _cells;
    private readonly int _rows;
    private readonly int _cols;

    public Generation(int rows, int cols)
    {
        _rows = rows;
        _cols = cols;
        _cells = new bool[rows * cols];
    }

    public bool HasLiveCells => Array.IndexOf(_cells, true) >= 0;
    public int Count => _cells.Length;
    public int Rows => _rows;
    public int Cols => _cols;

    public IEnumerable<RowCol> Keys
    {
        get
        {
            for (int r = 0; r < _rows; r++)
            {
                for (int c = 0; c < _cols; c++)
                {
                    yield return new RowCol(r, c);
                }
            }
        }
    }

    public IEnumerable<bool> Values
    {
        get
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                yield return _cells[i];
            }
        }
    }

    public bool this[RowCol key]
    {
        get => _cells[key.Row * _cols + key.Col];
        set => _cells[key.Row * _cols + key.Col] = value;
    }

    public void Add(RowCol key, bool value) => this[key] = value;

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

    public IEnumerator<KeyValuePair<RowCol, bool>> GetEnumerator()
    {
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cols; c++)
            {
                yield return new KeyValuePair<RowCol, bool>(new RowCol(r, c), _cells[r * _cols + c]);
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
