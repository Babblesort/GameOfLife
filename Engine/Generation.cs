using System.Collections;

namespace Engine;

public class Generation : IEnumerable<KeyValuePair<RowCol, bool>>
{
    private readonly Dictionary<RowCol, bool> _cells = new();

    public bool HasLiveCells => _cells.Values.Any(v => v);
    public int Count => _cells.Count;
    public ICollection<RowCol> Keys => _cells.Keys;
    public ICollection<bool> Values => _cells.Values;

    public bool this[RowCol key]
    {
        get => _cells[key];
        set => _cells[key] = value;
    }

    public void Add(RowCol key, bool value) => _cells.Add(key, value);
    public void Remove(RowCol key) => _cells.Remove(key);

    public IEnumerable<string> ToCsv() =>
        _cells.Select(kv => $"{kv.Key.ToCsv()},{kv.Value}");

    public IEnumerator<KeyValuePair<RowCol, bool>> GetEnumerator() => _cells.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
