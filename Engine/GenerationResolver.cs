namespace Engine;

public static class GenerationResolver
{
    public static void ResolveNextGeneration(Grid grid, Rules rules, Generation current, Generation next)
    {
        int rows = grid.RowCount;
        int cols = grid.ColCount;
        bool[] src = current.Raw;
        bool[] dst = next.Raw;
        int surviveMask = rules.SurviveMask;
        int birthMask = rules.BirthMask;

        for (int r = 0; r < rows; r++)
        {
            int up   = r == 0        ? rows - 1 : r - 1;
            int down = r == rows - 1 ? 0        : r + 1;
            for (int c = 0; c < cols; c++)
            {
                int left  = c == 0        ? cols - 1 : c - 1;
                int right = c == cols - 1 ? 0        : c + 1;

                int n = 0;
                if (src[up   * cols + left ]) { n++; }
                if (src[up   * cols + c    ]) { n++; }
                if (src[up   * cols + right]) { n++; }
                if (src[r    * cols + left ]) { n++; }
                if (src[r    * cols + right]) { n++; }
                if (src[down * cols + left ]) { n++; }
                if (src[down * cols + c    ]) { n++; }
                if (src[down * cols + right]) { n++; }

                bool alive = src[r * cols + c];
                dst[r * cols + c] = alive
                    ? ((surviveMask >> n) & 1) != 0
                    : ((birthMask   >> n) & 1) != 0;
            }
        }
    }

    public static int NeighborsCount(RowCol cell, Grid grid, Generation cells)
    {
        var count = 0;
        count += LiveCellAtLocation(grid.NeighborTL(cell), cells);
        count += LiveCellAtLocation(grid.NeighborTT(cell), cells);
        count += LiveCellAtLocation(grid.NeighborTR(cell), cells);
        count += LiveCellAtLocation(grid.NeighborLL(cell), cells);
        count += LiveCellAtLocation(grid.NeighborRR(cell), cells);
        count += LiveCellAtLocation(grid.NeighborBL(cell), cells);
        count += LiveCellAtLocation(grid.NeighborBB(cell), cells);
        count += LiveCellAtLocation(grid.NeighborBR(cell), cells);
        return count;
    }

    public static int LiveCellAtLocation(RowCol location, Generation cells) => cells[location] ? 1 : 0;

    public static bool CellAliveNextGen(bool alive, int neighborCount, Rules rules)
    {
        var survives = alive && rules.SurviveNeighborCounts.Contains(neighborCount);
        var born = !alive && rules.BirthNeighborCounts.Contains(neighborCount);
        return survives || born;
    }
}
