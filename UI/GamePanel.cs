using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Engine;
using EngineGrid = Engine.Grid;

namespace UI;

public class GamePanel : Control
{
    private static readonly IPen GridLinePen = new Pen(Brushes.LightGray);
    private static readonly IBrush CellBrush = new SolidColorBrush(new Color(180, 34, 139, 34));

    private EngineGrid? _grid;
    private Generation? _cells;

    public event EventHandler<CellClickedEventArgs>? GridCellClicked;

    private int RowCount => _grid?.RowCount ?? 1;
    private int ColCount => _grid?.ColCount ?? 1;
    private double CellHeight => (Bounds.Height - 5) / RowCount;
    private double CellWidth => (Bounds.Width - 5) / ColCount;

    public EngineGrid? Grid
    {
        get => _grid;
        set
        {
            if (_grid != null)
                _grid.PropertyChanged -= OnGridPropertyChanged;
            _grid = value;
            if (_grid != null)
                _grid.PropertyChanged += OnGridPropertyChanged;
            InvalidateVisual();
        }
    }

    public Generation? Cells
    {
        get => _cells;
        set { _cells = value; InvalidateVisual(); }
    }

    private void OnGridPropertyChanged(object? sender, PropertyChangedEventArgs e) =>
        InvalidateVisual();

    public override void Render(DrawingContext context)
    {
        if (_grid == null) return;

        var gridWidth = ColCount * CellWidth;
        var gridHeight = RowCount * CellHeight;

        for (int r = 0; r <= RowCount; r++)
            context.DrawLine(GridLinePen, new Point(0, r * CellHeight), new Point(gridWidth, r * CellHeight));

        for (int c = 0; c <= ColCount; c++)
            context.DrawLine(GridLinePen, new Point(c * CellWidth, 0), new Point(c * CellWidth, gridHeight));

        if (_cells != null)
        {
            foreach (var cell in _cells)
            {
                if (!cell.Value) continue;
                context.DrawRectangle(CellBrush, null,
                    new Rect(cell.Key.Col * CellWidth, cell.Key.Row * CellHeight, CellWidth, CellHeight));
            }
        }
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        var pos = e.GetPosition(this);
        var col = (int)(pos.X / CellWidth);
        var row = (int)(pos.Y / CellHeight);
        GridCellClicked?.Invoke(this, new CellClickedEventArgs { Cell = new RowCol(row, col) });
    }
}
