namespace Engine
{
    public static class GenerationResolver
    {
        public static Generation ResolveNextGeneration(Grid grid, Rules rules, Generation cells)
        {
            var nextGen = new Generation();
            foreach (var cell in cells)
            {
                nextGen.Add(cell.Key, CellAliveNextGen(cell.Value, NeighborsCount(cell.Key, grid, cells), rules));
            }
            return nextGen;
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

        public static int LiveCellAtLocation(RowCol location, Generation cells) =>  cells[location] ? 1 : 0;

        public static bool CellAliveNextGen(bool alive, int neighborCount, Rules rules)
        {
            var survives = alive && rules.SurviveNeighborCounts.Contains(neighborCount);
            var born = !alive && rules.BirthNeighborCounts.Contains(neighborCount);

            return survives || born;
        }
    }
}
