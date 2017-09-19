using System;
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
        private float GridWidth => ColCount * CellWidth;
        private float GridHeight => RowCount * CellHeight;
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

            var rowBoxes = new RectangleF[(int)Math.Ceiling((float)RowCount / 2)];
            var counter = 0;
            for (var r = 0; r < RowCount; r += 2)
            {
                rowBoxes[counter++] = new RectangleF(0, r * CellHeight, GridWidth, CellHeight);
            }
            if (RowCount % 2 == 0)
            {
                e.Graphics.DrawLine(GridLinePen, 0, GridHeight, GridWidth, GridHeight);
            }

            var colBoxes = new RectangleF[(int)Math.Ceiling((float)ColCount / 2)];
            counter = 0;
            for (var c = 0; c < ColCount; c += 2)
            {
                colBoxes[counter++] = new RectangleF(c * CellWidth, 0, CellWidth, RowCount * CellHeight);
            }
            if (ColCount % 2 == 0)
            {
                e.Graphics.DrawLine(GridLinePen, GridWidth, 0, GridWidth, RowCount * CellHeight);
            }

            e.Graphics.DrawRectangles(GridLinePen, rowBoxes);
            e.Graphics.DrawRectangles(GridLinePen, colBoxes);

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
