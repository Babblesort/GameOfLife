using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Engine;

namespace UI
{
    public partial class GamePanel : Panel
    {
        private int RowCount => _grid.RowCount;
        private int ColCount => _grid.ColCount;
        private float CellHeight => (float)(Height-5) / _grid.RowCount;
        private float CellWidth => (float)(Width-5) / _grid.ColCount;
        private static readonly Pen GridLinePen = Pens.LightGray;
        private static readonly Brush CellBrush = new SolidBrush(Color.FromArgb(180, Color.ForestGreen));
        private Grid _grid;
        private Generation _cells;

        public Grid Grid
        {
            get
            {
                return _grid;
            }
            set
            {
                _grid = value;
                Refresh();
            }
        }
        public Generation Cells
        {
            get
            {
                return _cells;
            }
            set
            {
                _cells = value;
                Refresh();
            }
        }

        public GamePanel()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_grid == null) return;

            for (var r=0; r < RowCount; r++)
            {
                for (var c=0; c < ColCount; c++)
                {
                    e.Graphics.DrawLine(GridLinePen, 0, r * CellHeight, ColCount * CellWidth, r * CellHeight);
                    e.Graphics.DrawLine(GridLinePen, c * CellWidth, 0, c * CellWidth, RowCount * CellHeight);
                }
            }

            if (_cells?.Count > 0)
            {
                var paintCells = _cells
                    .Where(c => c.Value)
                    .Select(c => new RectangleF(c.Key.Col * CellWidth, c.Key.Row * CellHeight, CellWidth, CellHeight))
                    .ToArray();

                e.Graphics.FillRectangles(CellBrush, paintCells);
            }
        }
    }
}
