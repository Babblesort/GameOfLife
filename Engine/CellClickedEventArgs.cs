using System;

namespace Engine
{
    public class CellClickedEventArgs : EventArgs
    {
        public RowCol Cell { get; set; }
    }
}
