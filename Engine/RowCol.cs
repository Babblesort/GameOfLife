using System;

namespace Engine
{
    public class RowCol : Tuple<int, int>
    {
        public RowCol(int row, int col) : base(row, col) { }

        public int Row => Item1;
        public int Col => Item2;

        public string ToCsv()
        {
            return $"{Item1},{Item2}";
        }
    }
}
