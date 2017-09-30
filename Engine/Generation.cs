using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public class Generation : Dictionary<RowCol, bool>
    {
        public bool HasLiveCells => Values.Any(v => v);

        public IEnumerable<string> ToCsv()
        {
            return this.Select(kv => $"{kv.Key.ToCsv()},{kv.Value.ToString()}");
        }
    }
}
