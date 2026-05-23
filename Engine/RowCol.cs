namespace Engine;

public readonly record struct RowCol(int Row, int Col)
{
    public string ToCsv() => $"{Row},{Col}";
}
