using System;

namespace Engine
{
    public class RowColTuple : Tuple<int, int>
    {
        public RowColTuple(int row, int col) : base(row, col) { }

        public int Row => Item1;
        public int Col => Item2;
    }
}
