namespace Engine;

public static class GenerationResolver
{
    /// <summary>
    /// Computes the next generation from <paramref name="current"/> and writes the result
    /// into <paramref name="destination"/>, overwriting it in place. Callers should reuse
    /// buffer instances across generations to avoid per-generation allocation.
    /// </summary>
    public static void ResolveNextGenerationInto(Grid grid, Rules rules, Generation current, Generation destination)
    {
        int rows = grid.RowCount;
        int cols = grid.ColCount;
        bool[] src = current.Raw;
        bool[] dst = destination.Raw;
        int surviveMask = rules.SurviveMask;
        int birthMask = rules.BirthMask;

        for (int r = 0; r < rows; r++)
        {
            int up = r == 0 ? rows - 1 : r - 1;
            int down = r == rows - 1 ? 0 : r + 1;
            for (int c = 0; c < cols; c++)
            {
                int left = c == 0 ? cols - 1 : c - 1;
                int right = c == cols - 1 ? 0 : c + 1;

                int n = 0;
                if (src[up * cols + left]) { n++; }
                if (src[up * cols + c]) { n++; }
                if (src[up * cols + right]) { n++; }
                if (src[r * cols + left]) { n++; }
                if (src[r * cols + right]) { n++; }
                if (src[down * cols + left]) { n++; }
                if (src[down * cols + c]) { n++; }
                if (src[down * cols + right]) { n++; }

                bool alive = src[r * cols + c];
                dst[r * cols + c] = alive
                    ? ((surviveMask >> n) & 1) != 0
                    : ((birthMask >> n) & 1) != 0;
            }
        }
    }
}
